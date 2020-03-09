import { Injectable, Inject } from '@angular/core';
import { Abstractservice } from './abstractservice.service';
import { User } from 'src/model/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService extends Abstractservice<User>{
  
constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='user';
    
    
  }
}
