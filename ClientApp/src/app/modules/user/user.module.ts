import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { SharedModule } from 'src/app/shareds/shared/shared.module';

@NgModule({
  declarations: [
    UserComponent,
    UserDetailsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: UserComponent},
      {path: ':id', component: UserDetailsComponent},
    ]),
    SharedModule
  ]
})
export class UserModule { }