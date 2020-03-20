import { Injectable, Inject } from '@angular/core';
import { WorkSpace } from 'src/model/WorkSpace';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserModel } from 'src/model/UserModel';

@Injectable({
  providedIn: 'root'
})
export class RoomService extends Abstractservice<UserModel>{
  verifiedRooms : any;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='workspace';
    
    
  }
  getInRoomUsers(roomId : string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(this.baseUrl + 'api/' + "User/" + "InRoom/" + roomId);
}
 

  
}
