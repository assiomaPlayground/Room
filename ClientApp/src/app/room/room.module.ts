import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RoomComponent } from './room.component';
import { RoomDetailsComponent } from './room-details/room-details.component';

@NgModule({
  declarations: [
    RoomComponent,
    RoomDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: RoomComponent},
      {path: '/:Id', component: RoomDetailsComponent}
    ]),
  ]
})
export class RoomModule { }
