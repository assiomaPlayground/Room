import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationRoutingModule, RouteComponents } from './reservation.routing'
import { SharedModule } from 'src/app/shareds/shared/shared.module';

@NgModule({
  declarations: [
    RouteComponents
  ],
  imports: [
    CommonModule,
    ReservationRoutingModule,
    SharedModule
  ]
})
export class ReservationModule { }
