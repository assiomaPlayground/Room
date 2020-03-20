import { UserModel } from "../UserModel";
import { WorkSpaceReservationDTO } from "./WorkSpaceReservationDTO";

export class FoundUserWorkSpaceDTO{
    User : UserModel;
    WorkSpaceReservation : WorkSpaceReservationDTO;
}