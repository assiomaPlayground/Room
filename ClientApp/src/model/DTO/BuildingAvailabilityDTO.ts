import { Building } from "../Building";
import { WorkSpaceReservationDTO } from "./WorkSpaceReservationDTO";

export class BuildingAvailabilityDTO{
    TargetBuilding: Building;
    Available : WorkSpaceReservationDTO[];
}