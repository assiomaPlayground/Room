using MongoDB.Driver;
using RoomService.DTO;
using RoomService.Models;
using RoomService.Models.Types;
using RoomService.Settings;
using RoomService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Services
{
    /// <summary>
    /// Service for Building collection crud ops in abstract
    /// </summary>
    public class BuildingService : AbstractMongoCrudService<Building>
    {
        private class BoolAndBuffer
        {
            public bool Result;
            public IEnumerable<Reservation> Buffer;
        }
        /// <summary>
        /// Join repository
        /// </summary>
        private readonly WorkSpaceService workSpaceService;
        private readonly IMongoCollection<WorkSpace> _workSpaceRepo;
        private readonly IMongoCollection<WorkSpaceReservations> _workSpaceReservationsRepo;
        private readonly IMongoCollection<Reservation> _reservationRepo;
        public BuildingService(IRoomServiceMongoSettings settings, WorkSpaceService workSpaceService)
        {
            base.Init(settings, settings.BuildingCollection);
            this.workSpaceService = workSpaceService;
            this._workSpaceReservationsRepo = Database.GetCollection<WorkSpaceReservations>(settings.ReservationCollection);
            this._workSpaceRepo = Database.GetCollection<WorkSpace>(settings.WorkSpaceCollection);
            this._reservationRepo = Database.GetCollection<Reservation>(settings.ReservationCollection);
        }
        /// <summary>
        /// Join Building RoomsId with workspace data
        /// </summary>
        /// <param name="id">The building id</param>
        /// <returns>BuildingSpacesDTO the building and his spaces data</returns>
        public BuildingSpacesDTO GetBuildingSpaces(string id)
        {
            var building = Read(id);
            var qres =
                from space in workSpaceService.Collection.AsQueryable()
                where building.Rooms.Contains(space.Id)
                select space;
            return new BuildingSpacesDTO { Id = building.Id, Map = building.Map, Name = building.Name, Rooms = qres.ToArray() };
        }
        public BuildingAvailabilityDTO GetAvailableBuildingSpaces(string rid, string id, DeltaTime interval)
        {
            var building = Read(id); if (building == null) return null;
            if (building.Rooms == null) return null;
            var Tester = new Reservation { Interval = interval, Owner = rid };
            
            var qres =
               from space in workSpaceService.Collection.AsQueryable()
               where building.Rooms.Contains(space.Id)
               select space;

            foreach (var workSpace in qres)
            {
                Tester.Target = workSpace.Id;
                var Result = AvailabilityHeper(Tester);
                if (Result.Result)
                {
                    var resultBuffer = new List<WorkSpaceAvailabilityDTO>();
                    foreach (var item in Result.Buffer)
                    {
                        var avbl = GetReservationByDeltaTimeAdWorkSpaceId(item.Target, item.Interval);
                        WorkSpace wrksp = null;
                        if (avbl == null)
                            wrksp = _workSpaceRepo.Find<WorkSpace>(ws => ws.Id == item.Target).FirstOrDefault();
                        resultBuffer.Add(new WorkSpaceAvailabilityDTO
                        {
                            TargetWorkSpace = avbl != null ? avbl.Room : wrksp,
                            Availability = avbl != null ? avbl.Room.AllSeats - avbl.Users : wrksp.AllSeats,
                            Interval = item.Interval
                        });
                    }
                    return new BuildingAvailabilityDTO { TargetBuilding = building, Available = resultBuffer };
                }
            }
            return null;
        }
        private BoolAndBuffer AvailabilityHeper(Reservation model)
        {
            model.Interval = model.Interval.Clamp();
            if (!model.Interval.IsValid())
                return new BoolAndBuffer { Result = false };
            var Buffer = new List<Reservation>();
            var Iterator = model.Clone();
            Iterator.Interval = Iterator.Interval.First();
            if (this.CanCreateReservation(Iterator))
                Buffer.Add(Iterator.Clone());
            else
                return new BoolAndBuffer { Result = false };
            
            Iterator.Interval.EndTime = model.Interval.EndTime;

            while ((Iterator.Interval = Iterator.Interval.Next()) != null)
            {
                if (this.CanCreateReservation(Iterator))
                    Buffer.Add(Iterator.Clone());
                else
                    return new BoolAndBuffer { Result = false };

                Iterator.Interval.EndTime = model.Interval.EndTime;
            }
            return new BoolAndBuffer { Result = true, Buffer = Buffer };
        }
        public bool CanCreateReservation(Reservation model)
        {
            if (!model.Interval.IsValid()) //Invalid
                return false;

            HashSet<Reservation.Statuses> filter = new HashSet<Reservation.Statuses>
            { Reservation.Statuses.ATTIVA, Reservation.Statuses.CHECKIN, Reservation.Statuses.INCORSO };
            var qres = from res in _reservationRepo.AsQueryable()
                       where model.Owner == res.Owner && filter.Contains(res.Status)
                       select res;

            var wsr = GetReservationByDeltaTimeAdWorkSpaceId(model.Target, model.Interval);
            if (wsr != null)
                if (wsr.Room.AllSeats <= wsr.Users)
                    return false;

            foreach (var userRes in qres)
                if (DataInsersects(userRes.Interval.StartTime, userRes.Interval.EndTime, model.Interval.StartTime, model.Interval.EndTime))
                    return false;

            return true;
        }
        private bool DataInsersects(string start, string end, string secStart, string secEnd)
        => !(string.Compare(start, secStart) < 0 ?
            string.Compare(end, secStart) < 0 : string.Compare(secEnd, start) < 0);
        public WorkSpaceReservationDTO GetReservationByDeltaTimeAdWorkSpaceId(string id, DeltaTime deltaTime)
        {
            var res = _workSpaceReservationsRepo.Find(wsr => wsr.Owner == id && wsr.Times.Equals(deltaTime)).FirstOrDefault();
            if (res == null)
                return null;
            return new WorkSpaceReservationDTO
            {
                Room = _workSpaceRepo.Find<WorkSpace>(wrk => wrk.Id == id).FirstOrDefault(),
                Times = res.Times,
                Users = res.Reservations.Count()
            };
        }
        public override DeleteResult Delete(string id)
        {
            workSpaceService.DeleteByBuildingId(id);//Cascade delete rooms in building
            return base.Delete(id);
        }
    }
}