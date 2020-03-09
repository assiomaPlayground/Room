import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserComponent } from './user.component';

@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      {path: 'user', component: UserComponent},
    ]),
  ]
})
export class UserModule { }
