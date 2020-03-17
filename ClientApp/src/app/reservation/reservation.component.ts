import { Component, OnInit } from '@angular/core';

import { ReservationsService } from 'src/service/reservations.service';
import { Reservation } from 'src/model/reservation';
import { BuildingService } from 'src/service/building.service';
import { Building } from 'src/model/building';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
  reservation: Reservation[];
  buildingList: Building[];
  selectedBuilding : Building;
  constructor(private service: ReservationsService,private buildingService: BuildingService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    this.service.List().subscribe(reservation=> this.reservation= reservation);
  }
  insert(reservation: Reservation){
    this.service.insert(reservation).subscribe(()=> this.list());
  }
  update(reservation: Reservation){
    this.service.update(reservation).subscribe(()=> this.list());
  }
  delete(reservation:Reservation){
    this.service.delete(reservation.Id).subscribe(()=> this.list());
  }
  verifica(){
   // this.service.wReservation()
  }
  getBuildingList(){
    this.buildingService.List().subscribe(building=> this.buildingList=building);
}
  setBuilding(building : Building){
    this.selectedBuilding = building;
  }
}
