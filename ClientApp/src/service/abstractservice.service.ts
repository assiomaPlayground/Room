import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getAllLifecycleHooks } from '@angular/compiler/src/lifecycle_reflector';
import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { Observable } from 'rxjs';
import { Service } from './service.service';


export class Abstractservice<Entity>implements Service<Entity> {
  
  type: String;
  port: String;
  constructor(protected http:HttpClient) { }

  
  read(id: String): Observable<Entity> {
    return  this.http.get<Entity>('');
  }
  delete(id: String): Observable<Entity> {
    return this.http.delete<Entity>('http://localhost:' + this.port + '/' + this.type + ''+ id);
  }
  update(entity: Entity): Observable<Entity> {
    return this.http.put<Entity>('http://localhost:' + this.port + '/' + this.type + '', entity)
  }
  insert(entity: Entity): Observable<any> {
    return this.http.post<Entity>('https://localhost:44397' + '/' + this.type + '/create', entity);
  }
  List(): Observable<Entity[]> {
    return this.http.get<Entity[]>('https://localhost:44397' +'/' + this.type + '/'+ 'getall');
    
   
  }
  

}