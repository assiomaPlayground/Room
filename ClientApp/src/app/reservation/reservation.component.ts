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
  startDate : Date = new Date();
  durata : Number = 0;
  reservation: Reservation[];
  buildingList: Building[];
  selectedBuilding : Building;
  rooms : any;
  constructor(private service: ReservationsService,private buildingService: BuildingService) { }

  ngOnInit() {
    this.list();
    this.getBuildingList();
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
    let endDate : Date;
    let startDate : Date; 
    switch (this.durata) {
      case 0: {
        startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),9);
        endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),13);
        break;
      }
      case 1: {
        startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),14);
        endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),18);
        break;

      }
      case 2: {
        startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),9);
        endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),18);
        break;
      }
        case 3: {
          startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),9);
          endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),18);
          endDate.setDate(this.startDate.getDate()+ 1);
          break;
        }
        case 4: {
          startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),9);
          endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),18);
          endDate.setDate(this.startDate.getDate()+ 2);
          break;
        }
        case 5: {
          startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),9);
          endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),18);
          endDate.setDate(this.startDate.getDate()+ 3);
          break;
        }
        case 6: {
          startDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),9);
          endDate = new Date(this.startDate.getFullYear(),this.startDate.getMonth(),this.startDate.getDate(),18);
          endDate.setDate(this.startDate.getDate()+ 4);
          break;   
        }
    
      default: return;
     }
    this.service.availableRooms( this.selectedBuilding.Id , startDate, endDate).subscribe(result => this.rooms=result);
  }
  getBuildingList(){
    this.service.getAllBuildings().subscribe(building=> this.buildingList=building);
}
  setBuilding(building : Building){
    this.selectedBuilding=building;
     
  }
  }
  

