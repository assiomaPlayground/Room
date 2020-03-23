import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RoomService } from 'src/service/room.service';

@Component({
  selector: 'app-room-details',
  templateUrl: './room-details.component.html',
  styleUrls: ['./room-details.component.css']
})
export class RoomDetailsComponent implements OnInit {
room : any[];
  constructor(private route : ActivatedRoute, private service : RoomService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => this.service.getInRoomUsers(params['id']).subscribe(room => this.room = room));
  }
  
}
