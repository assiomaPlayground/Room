import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/service/room.service';
import { Room } from 'src/model/room';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit {
  room: Room[];
  roomins: Room= new Room;
  constructor(private service: RoomService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    this.service.List().subscribe(room=> this.room=room);
  }
  insert(room: Room){
    this.service.insert(room).subscribe(()=>this.list());
  }
  update(room: Room){
    this.service.update(room).subscribe(()=> this.list());
  }
  delete(room: Room){
    this.service.delete(room.Id).subscribe(()=> this.list());

  }
}
