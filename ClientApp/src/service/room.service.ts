import { Injectable, Inject } from '@angular/core';
import { Room } from 'src/model/room';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/model/user';

@Injectable({
  providedIn: 'root'
})
export class RoomService extends Abstractservice<Room>{
  verifiedRooms : any[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='workspace';
    
    
  }
  getInRoomUsers(roomId : string): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'api/' + "User/" + "InRoom/" + roomId);
}
 

  
}
