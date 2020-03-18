import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { User } from 'src/model/user';
import { Reservation } from 'src/model/reservation';
import { WorkspaceReservation } from 'src/model/workspaceResevation';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  
  public wreservations: Array<any> = new Array<any>();

  constructor(private service: ReservationsService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    let user = JSON.parse(localStorage.getItem('currentUser'));
    this.service.userReservationsWorkSpaceavailability(user.Id).subscribe(res => {
      this.wreservations = res;
    })
  }
}





