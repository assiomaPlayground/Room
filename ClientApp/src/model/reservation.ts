
import { Timestamp } from "rxjs";
import { Data } from "@angular/router";
import { Time } from "@angular/common";
import { Statuses } from "./Statuses";
import { DeltaTime } from "./Types/DeltaTime";

export class Reservation{
    Id: string;
    Status: Statuses;
    CheckIn: string[];
    CheckOut: string[];
    Owner: string;
    Target: string;
    EndTime: Date;
    Interval : DeltaTime ;
    ReservationSocket: string;
}