import { Observable } from "rxjs";

export interface Service<Entity> {

  read(id: String): Observable<Entity> ;

  delete(id: String): Observable<Entity>;

  update(entity: Entity,Id:String): Observable<Entity> ;

  insert(entity: Entity): Observable<Entity>;

  List(): Observable<Entity[]>;

}