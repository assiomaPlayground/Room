import { Injectable, Inject } from '@angular/core';
import { Abstractservice } from './abstractservice.service';
import { UserModel } from 'src/model/UserModel';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class UserService extends Abstractservice<UserModel>{
  
constructor(http: HttpClient, @Inject('BASE_URL') baseUrl : String) { 
    super(http, baseUrl)
    this.type='user';
    
    
  }
  register(user: UserModel): Observable<any> {
    return this.http.post<UserModel>(this.baseUrl +this.type +'/Register',user);
}
wUser(id: string):Observable<any>{
  return this.http.get<any>(this.baseUrl+'api/'+this.type+'/find'+'/'+id)
}
}
