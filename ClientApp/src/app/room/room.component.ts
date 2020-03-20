import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/service/room.service';
import { WorkSpace } from 'src/model/WorkSpace';
import { Router } from '@angular/router';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit {
  roomList: Array<any> = new Array<any>();
  selectedRoom: any;
  building : any;
  
  constructor(private service: RoomService, private router : Router) { }

  ngOnInit() {
    if(this.service.verifiedRooms == null){
      this.router.navigate(['reservation']);
    }
    else{
      this.roomList = this.service.verifiedRooms.Available;
      this.building = this.service.verifiedRooms.TargetBuilding;
      this.service.verifiedRooms = null;
    }
      
  }
  
  
}

