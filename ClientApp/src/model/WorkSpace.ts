import { Point2d } from "./Types/Point2d";

export class WorkSpace{
    Id : string;
    Name : string;
    Features : string[];
    AllSeats : number;
    SubMap : string;
    Building : string;
    Pivot : Point2d;
}