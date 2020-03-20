import { UserTypes } from "./UserTypes";

export class UserModel{
    Id: string;
    Username: string;
    Password: string;
    Usertype: UserTypes;
    Photo: string;
    Token : string;
}