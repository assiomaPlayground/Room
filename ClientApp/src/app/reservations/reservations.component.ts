import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { User } from 'src/model/user';
import { Reservation } from 'src/model/reservation';
import { WorkspaceReservation } from 'src/model/workspaceResevation';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
reservations: Reservation[];
wreservations: WorkspaceReservation[];

  constructor(private service: ReservationsService) { }

  ngOnInit() {
    this.list();
    this.wreservation();
  }
  list(){
    const res= JSON.parse(localStorage.getItem('currentUser')) ;
      
    this.service.reseravtion(res.Id).subscribe(reservations=>this.reservations=reservations );
  }
  wreservation(){
    for(var reservations of this.reservations){
      this.service.wReservation(reservations.Target,reservations.EndTime).subscribe(wreservation=>this.wreservations=wreservation)
    }
  }
}



