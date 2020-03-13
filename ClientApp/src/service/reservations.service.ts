import { Injectable, Inject } from '@angular/core';
import { Reservation } from 'src/model/reservation';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/model/user';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService extends Abstractservice<Reservation>{
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='reservation';
    
    
  }
  
  reseravtion(id: String): Observable<any>{
    

    return this.http.get<Reservation>(this.baseUrl +'api/'+this.type + '/user/'+ id);
  }

}
