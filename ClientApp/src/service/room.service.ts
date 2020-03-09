import { Injectable, Inject } from '@angular/core';
import { Room } from 'src/model/room';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoomService extends Abstractservice<Room>{
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='workspace';
    
    
  }
}
