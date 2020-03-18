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
reservations: any[];
wreservations: any[];

  constructor(private service: ReservationsService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    const res= JSON.parse(localStorage.getItem('currentUser')) ;
   
    this.service.reservation(res.Id).subscribe(reservations=>{
      this.reservations=reservations; 
      this.reservations.forEach(reservation => {
       
       
        this.service.wReservation(
          reservation.Target,
          reservation.Interval.StartTime,
          reservation.Interval.EndTime,
          
        )
        
        
        .subscribe(wreservation=>{
          this.wreservations=wreservation;
          this.wreservations.forEach(w=>{
            localStorage.setItem('prova',JSON.stringify(w))
          })
        });
      });  
    });
 
   
  }
}





