import { Injectable, Inject } from '@angular/core';
import { Reservation } from 'src/model/reservation';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService extends Abstractservice<Reservation>{
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='user';
    
    
  }
}
