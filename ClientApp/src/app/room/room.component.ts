import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/service/room.service';
import { Room } from 'src/model/room';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit {
  rooms: any[];
  
  constructor(private service: RoomService) { }

  ngOnInit() {
    this.rooms = this.service.verifiedRooms;

  }
  
  
}

