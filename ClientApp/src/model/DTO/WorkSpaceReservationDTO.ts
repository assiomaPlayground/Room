import { WorkSpace } from "../WorkSpace";
import { DeltaTime } from "../Types/DeltaTime";
import { Reservation } from "../Reservation";

export class WorkSpaceReservationDTO{
    ReservationId : String;
    WorkSpace : WorkSpace;
    Interval : DeltaTime;
    Status : number;
    Users : number;
}