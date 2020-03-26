import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getAllLifecycleHooks } from '@angular/compiler/src/lifecycle_reflector';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { Observable } from 'rxjs';
import { Service } from './service.service';


export class Abstractservice<Entity>implements Service<Entity> {
  protected  baseUrl: String;
  protected type: string;
  protected port: string;
  constructor(protected http:HttpClient, @Inject('BASE_URL') baseUrl : String) {
    this.baseUrl=baseUrl
   }

  
  read(id: String): Observable<Entity> {
    return  this.http.get<Entity>(this.baseUrl+'api/'+this.type+'/'+ id);
  }
  delete(id: String): Observable<Entity> {
    return this.http.delete<Entity>(this.baseUrl + 'api/'+this.type +'/'+ id);
  }
  update(entity: Entity,Id:String): Observable<Entity> {
    return this.http.put<Entity>(this.baseUrl + 'api/'+ this.type +'/'+Id, entity)
  }
  insert(entity: Entity): Observable<any> {
    return this.http.post<Entity>(this.baseUrl + 'api/'+this.type ,entity);
  }
  List(): Observable<Entity[]> {
    return this.http.get<Entity[]>(this.baseUrl +'api/'+  this.type );
    
   
  }
  

}