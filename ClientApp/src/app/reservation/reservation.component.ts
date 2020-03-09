import { Component, OnInit } from '@angular/core';

import { ReservationsService } from 'src/service/reservations.service';
import { Reservation } from 'src/model/reservation';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
  reservation: Reservation[];
  constructor(private service: ReservationsService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    this.service.List().subscribe(reservation=> this.reservation= reservation);
  }
  insert(reservation: Reservation){
    this.service.insert(reservation).subscribe(()=> this.list());
  }
  update(reservation: Reservation){
    this.service.update(reservation).subscribe(()=> this.list());
  }
  delete(reservation:Reservation){
    this.service.delete(reservation.Id).subscribe(()=> this.list());
  }
}

