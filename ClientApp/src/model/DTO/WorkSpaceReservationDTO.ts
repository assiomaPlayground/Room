import { WorkSpace } from "../WorkSpace";
import { DeltaTime } from "../Types/DeltaTime";

export class WorkSpaceReservationDTO{
    WorkSpace : WorkSpace;
    Interval : DeltaTime;
    Users : number;
}