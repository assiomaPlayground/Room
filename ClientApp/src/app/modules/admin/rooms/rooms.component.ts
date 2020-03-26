import { Component, OnInit } from '@angular/core';
import { WorkSpace } from 'src/model/WorkSpace';
import { RoomService } from 'src/service/room.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {

  
  rooms: WorkSpace[];
  roomtoinsert: WorkSpace = new WorkSpace();

  constructor(private service: RoomService) { }

  ngOnInit() {
    this.getrooms();
  }

  getrooms() {
    this.service.List().subscribe(rooms => this.rooms = rooms);
  }

  delete(rooms: WorkSpace) {
    this.service.delete(rooms.Id).subscribe(() => this.getrooms());
  }

  update(rooms: WorkSpace, Id : String) {
    this.service.update(rooms,rooms.Id).subscribe(() => this.getrooms());
  }

  insert(rooms: WorkSpace) {
    this.service.insert(rooms).subscribe(() => this.getrooms());
  }

  clear(){
    this.roomtoinsert = new WorkSpace();
  }
}

