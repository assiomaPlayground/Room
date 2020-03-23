import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReservationComponent } from './reservation/reservation.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';

const appRoutes: Routes = [ 
    { path: '', component: ReservationsComponent},
    { path: 'reservation', component: ReservationComponent},
    { path:'reservationDetails', component: ReservationDetailsComponent}
]

export const RouteComponents : any[] = [
    ReservationsComponent,
    ReservationComponent,
    ReservationDetailsComponent,
];


@NgModule({
    imports: [RouterModule.forChild(appRoutes)],
    exports: [RouterModule]
  })
export class ReservationRoutingModule { }