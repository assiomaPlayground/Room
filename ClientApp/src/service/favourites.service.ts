import { Injectable } from '@angular/core';
import { Abstractservice } from './abstractservice.service';
import { Favourites } from 'src/model/favourites';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FavouritesService extends Abstractservice<Favourites>{
  
  constructor(http: HttpClient) { 
    super(http)
    this.type='';
    this.port='';
  }
}
