using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Models;
using RoomService.Models.Types;

namespace RoomService.Utils
{
    public static class DeltaTimeExtensions
    {
        //@TODO Use settings
        private readonly static int _morningStartHour = 9;
        private readonly static int _morningEndHour = 13;
        private readonly static int _afternoonStartHour = 14;
        private readonly static int _afternoonEndHour = 18;
        public static bool IsValid(this DeltaTime deltaTime)
            => (string.Compare(deltaTime.StartTime, deltaTime.EndTime) < 0);
        public static DeltaTime Clamp(this DeltaTime delta)
        {
            DateTime start = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            var StartMorning   = new DateTime(start.Year, start.Month, start.Day, _morningStartHour,   0, 0, start.Kind);
            var StartAfternoon = new DateTime(start.Year, start.Month, start.Day, _afternoonStartHour, 0, 0, start.Kind);
            start = HourDistance(start, StartMorning) < HourDistance(start, StartAfternoon) ? StartMorning : StartAfternoon;

            DateTime end = DateTime.Parse(delta.EndTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            var EndMorning   = new DateTime(end.Year, end.Month, end.Day, _morningEndHour,   0, 0, end.Kind);
            var EndAfternoon = new DateTime(end.Year, end.Month, end.Day, _afternoonEndHour, 0, 0, end.Kind);
            end = HourDistance(end, EndMorning) < HourDistance(end, EndAfternoon) ? EndMorning : EndAfternoon;

            return new DeltaTime { StartTime = start.ToString("o"), EndTime = end.ToString("o") };
        }
        public static DeltaTime First(this DeltaTime delta)
        {
            DateTime start = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DateTime end   = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind); ;
            if (start.Hour == _morningStartHour)
                end = end.AddHours(_morningEndHour - start.Hour);
            if (start.Hour == _afternoonStartHour)
                end = end.AddHours(_afternoonEndHour - start.Hour);

            return new DeltaTime { StartTime = start.ToString("o"), EndTime = end.ToString("o") };
        }
        public static DeltaTime Next(this DeltaTime delta) 
        {
            DateTime start = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DateTime end   = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);

            DateTime last = DateTime.Parse(delta.EndTime, null, System.Globalization.DateTimeStyles.RoundtripKind);

            if (start.Hour == _morningStartHour)
            {
                start = start.AddHours(_afternoonStartHour - _morningStartHour);
                end   = end  .AddHours(_afternoonEndHour   - _morningStartHour);
            }
            else
            {
                start = start.AddDays(1).AddHours(_morningStartHour - _afternoonStartHour);
                end   = end  .AddDays(1).AddHours(_morningEndHour   - _afternoonStartHour);
            }

            if (end > last)
                return null;

            return new DeltaTime { StartTime = start.ToString("o"), EndTime = end.ToString("o") };
        }
        private static double HourDistance(DateTime a, DateTime b)
            => Math.Abs((a - b).TotalHours);      
    }
}
