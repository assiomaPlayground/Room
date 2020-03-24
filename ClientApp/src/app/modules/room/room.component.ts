import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/service/room.service';

import { Router } from '@angular/router';
import { WorkSpace } from 'src/model/WorkSpace';
import { Reservation } from 'src/model/Reservation';
import { Building } from 'src/model/Building';
import { DeltaTime } from 'src/model/Types/DeltaTime';
import { AuthenticationService } from 'src/service/authenticationservice.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit {
  roomList: Array<WorkSpace> = new Array<WorkSpace>();
  selectedRoom: WorkSpace;
  building: Building;
  room: WorkSpace;
  interval: DeltaTime;
  constructor(private service: RoomService, private router: Router, private authService: AuthenticationService) { }

  ngOnInit() {
    if (this.service.verifiedRooms == null) {
      this.router.navigate(['reservation']);
    }
    else {
      this.roomList = this.service.verifiedRooms.WorkSpaces;
      this.building = this.service.verifiedRooms.Building;
      this.interval = this.service.interval;
      this.service.interval = null;
      this.service.verifiedRooms = null;
    }

  }
  setRoom(room: WorkSpace) {
    this.selectedRoom = room;
  }
  conferma() {
    let reservation: Reservation = new Reservation();
    reservation.Interval = this.interval;
    reservation.Status = 0;
    reservation.Owner = this.authService.currentUserValue.Id;
    reservation.Target = this.selectedRoom.Id;
    this.service.confirmRoom(reservation).subscribe(res =>
      this.router.navigate(['reservation'])
    );


  }


}

