import { Injectable } from '@angular/core';
import { Room } from 'src/model/room';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoomService extends Abstractservice<Room>{
  
  constructor(http: HttpClient) { 
    super(http)
    this.type='';
    this.port='';
  }
}
