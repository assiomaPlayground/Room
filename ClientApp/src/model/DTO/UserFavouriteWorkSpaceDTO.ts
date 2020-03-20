import { Favourites } from "../Favourites";
import { WorkSpace } from "../WorkSpace";
import { UserModel } from "../UserModel";

export class UserFavouriteWorkSpaceDTO{
    User : UserModel;
    Favourites : FavouriteWorkSpaceDTO[];
}

export class FavouriteWorkSpaceDTO{
    Favourite : Favourites;
    WorkSpace : WorkSpace;
}