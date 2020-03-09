import { Injectable } from '@angular/core';
import { Building } from 'src/model/building';
import { Abstractservice } from './abstractservice.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BuildingService extends Abstractservice<Building>{
  
  constructor(http: HttpClient) { 
    super(http)
    this.type='';
    this.port='';
  }
}
