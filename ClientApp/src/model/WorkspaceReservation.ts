import { DeltaTime } from "./Types/DeltaTime";

export class WorkspaceReservation {
    id : string;
    Interval: DeltaTime;
    Owner: string;
    Reservations : number;
}