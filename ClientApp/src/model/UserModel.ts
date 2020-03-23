import { UserTypes } from "./UserTypes";

export class UserModel{
    Id: string;
    Username: string;
    Password: string;
    UserType: UserTypes;
    Photo: string;
    Token : string;
}