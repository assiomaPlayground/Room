import { Injectable } from '@angular/core';
import { Abstractservice } from './abstractservice.service';
import { QrMap } from 'src/model/qrmap';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QrmapService extends Abstractservice<QrMap>{
  
  constructor(http: HttpClient) { 
    super(http)
    this.type='';
    this.port='';
  }
}
