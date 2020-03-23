import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RoomComponent } from './room.component';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { SharedModule } from 'src/app/shareds/shared/shared.module';

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
    SharedModule
  ]
})
export class RoomModule { }
