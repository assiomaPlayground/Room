
import { Timestamp } from "rxjs";
import { Data } from "@angular/router";
import { Time } from "@angular/common";
import { Status } from "./status";

export class Reservation{
    Id: String;
    Status: Status;
    In: Data;
    Out: Data;
    OutDef: Data;
    StartTime: Data;
    EndTime: Data;
    Owner : String ;
    Target: String;
    

}