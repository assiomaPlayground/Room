import { Injectable, Inject } from '@angular/core';
import { Reservation } from 'src/model/reservation';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/model/user';
import { Data } from '@angular/router';
import { BuildingService } from './building.service';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';

@Injectable({
  providedIn: 'root'
})
export class ReservationsService extends Abstractservice<Reservation>{
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='reservation';
    
    
  }
  
  reservation(id: String): Observable<any>{

    return this.http.get<Reservation>(this.baseUrl +'api/'+this.type + '/user/'+ id);
  }

  wReservation(id: String, start: String, end: String):Observable<any>{
    return this.http.get<any>(this.baseUrl+'api/'+this.type+'/workspace/'+id+'/'+start+'/'+end )
  }

  checkIn(roomId : String, date : String): Observable<any>{
    return this.http.post<any>(this.baseUrl + 'api/'+this.type + "checkIn",{ "WorkSpaceId" : roomId,"Date" : date });

  }
  availableRooms(buildingId: String, startDate : Date, endDate: Date): Observable<any>{

    return this.http.post<any>(this.baseUrl +'api/'+ "Building/" + "CheckAvailability/" + buildingId,{ "StartTime" : startDate.toISOString(),"EndTime" : endDate.toISOString()});

  
  }
} 
