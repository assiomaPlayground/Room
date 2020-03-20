import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';
import { WorkSpaceReservationDTO } from 'src/model/DTO/WorkSpaceReservationDTO';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  
  public wreservations: Array<WorkSpaceReservationDTO> = new Array<WorkSpaceReservationDTO>();

  constructor(private service: ReservationsService) { }

  ngOnInit() {
    this.list();
  }
  list(){
    let user = JSON.parse(localStorage.getItem('currentUser'));
    this.service.userReservationsWorkSpaceavailability(user.Id).subscribe(res => {
      this.wreservations = res;
    })
  }
  delete(reservation: any){
    this.service.delete( reservation.ReservationId).subscribe(()=> this.list)
  }
}





