import { Injectable, Inject } from '@angular/core';
import { Abstractservice } from './abstractservice.service';
import { Favourites } from 'src/model/Favourites';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FavouritesService extends Abstractservice<Favourites>{
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='favourites';
    
    
  }
}
