import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { UserDetailsComponent } from '../user/user-details/user-details.component';

@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      {path: 'user', component: UserComponent},
      {path: 'user/:id', component: UserDetailsComponent},
    ]),
  ]
})
export class UserModule { }
