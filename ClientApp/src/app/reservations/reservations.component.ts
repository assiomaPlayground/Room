import { Component, OnInit } from '@angular/core';
import { ReservationsService } from 'src/service/reservations.service';
import { WorkSpaceReservationDTO } from 'src/model/DTO/WorkSpaceReservationDTO';
import { UserModel } from 'src/model/UserModel';
import { AuthenticationService } from 'src/service/authenticationservice.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  
  public wreservations: Array<WorkSpaceReservationDTO> = new Array<WorkSpaceReservationDTO>();
  public user : UserModel;
  
  constructor(private service: ReservationsService, private authService : AuthenticationService) { }

  ngOnInit() {
    this.user = this.authService.currentUserValue;
    this.list();
  }
  list(){
    this.service.userReservationsWorkSpaceavailability(this.user.Id).subscribe(res => {
      this.wreservations = res;
    })
  }
  delete(reservation: any){
    this.service.delete( reservation.ReservationId).subscribe(()=> this.list)
  }
}





