import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, config } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserModel } from 'src/model/UserModel';
import { Abstractservice } from './abstractservice.service';



@Injectable({ providedIn: 'root' })
export class AuthenticationService extends Abstractservice<UserModel> {
    private currentUserSubject: BehaviorSubject<UserModel>;
    public currentUser: Observable<UserModel>;
  

    constructor(protected http: HttpClient,@Inject('BASE_URL') baseUrl : String) {
      super(http, baseUrl);
        this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
       
      }

    public get currentUserValue(): UserModel {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(this.baseUrl+ 'api'+`/user/token`, { username, password })
            .pipe(map(user => {
                
                // login successful if there's a jwt token in the response
               
                   
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    
                    this.currentUserSubject.next(user);
              
             
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
  }