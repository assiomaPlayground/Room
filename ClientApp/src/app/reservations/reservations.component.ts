import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { UserModel } from 'src/model/UserModel';
import { Reservation } from 'src/model/Reservation';
import { WorkspaceReservation } from 'src/model/WorkspaceReservation';
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
  delete(reservation: any){
   
    this.service.delete( reservation.ReservationId).subscribe(()=> this.list)
  }
}





