import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FavouritesComponent } from './favourites.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      {path: 'favourites', component: FavouritesComponent},
    ]),
  ]
})
export class FavouritesModule { }
