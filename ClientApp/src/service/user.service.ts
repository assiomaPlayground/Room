import { Injectable, Inject } from '@angular/core';
import { Abstractservice } from './abstractservice.service';
import { User } from 'src/model/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class UserService extends Abstractservice<User>{
  
constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='user';
    
    
  }
  register(user: User): Observable<any> {
    return this.http.post<User>(this.baseUrl +this.type +'/Register',user);
}
}
