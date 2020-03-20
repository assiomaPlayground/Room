import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';

@Component({
  selector: 'app-qrmap',
  templateUrl: './qrmap.component.html',
  styleUrls: ['./qrmap.component.css']
})
export class QrmapComponent  {
  ;
  constructor(private rService : ReservationsService) { }
  scanSuccessHandler(event:any){
    this.rService.checkIn(event,new Date(Date.now()).toISOString());
  
  }
 
}
