import { Injectable, Inject } from '@angular/core';
import { Building } from 'src/model/Building';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BuildingService extends Abstractservice<Building>{
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='building';
    
    
  }
  
}
