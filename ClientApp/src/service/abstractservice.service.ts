import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getAllLifecycleHooks } from '@angular/compiler/src/lifecycle_reflector';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { Observable } from 'rxjs';
import { Service } from './service.service';


export class Abstractservice<Entity>implements Service<Entity> {
  protected baseUrl : string;
  protected type: string;
  protected port: string;
  constructor(protected http:HttpClient, @Inject('BASE_URL') baseUrl: string) {
   this.baseUrl=baseUrl; 
   }

  
  read(id: String): Observable<Entity> {
    return  this.http.get<Entity>('');
  }
  delete(id: String): Observable<Entity> {
    return this.http.delete<Entity>(this.baseUrl + this.type + '/Delete'+ id);
  }
  update(entity: Entity): Observable<Entity> {
    return this.http.put<Entity>(this.baseUrl + this.type + '/Update', entity)
  }
  insert(entity: Entity): Observable<any> {
    return this.http.post<Entity>(this.baseUrl + this.type + '/Create',entity);
  }
  List(): Observable<Entity[]> {
    return this.http.get<Entity[]>(this.baseUrl + this.type + '/Getall');
    
   
  }
  

}