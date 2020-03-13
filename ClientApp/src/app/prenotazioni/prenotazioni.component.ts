import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { User } from 'src/model/user';
import { Reservation } from 'src/model/reservation';

@Component({
  selector: 'app-prenotazioni',
  templateUrl: './prenotazioni.component.html',
  styleUrls: ['./prenotazioni.component.css']
})
export class PrenotazioniComponent implements OnInit {
reservations: Reservation[];
  constructor(private service: ReservationsService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    const res= JSON.parse(localStorage.getItem('currentUser')) ;
      
    this.service.reseravtion(res.Id).subscribe(reservations=>this.reservations=reservations );
  }
}



