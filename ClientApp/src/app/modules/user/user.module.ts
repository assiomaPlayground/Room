import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserComponent } from './user.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { SharedModule } from 'src/app/shareds/shared/shared.module';
import { QrmapComponent } from './qrmap/qrmap.component';
import { FavouritesComponent } from './favourites/favourites.component';
import { ZXingScannerModule } from 'src/app/modules/user/scanner/zxing-scanner.module'

@NgModule({
  declarations: [
    UserComponent,
    UserDetailsComponent,
    QrmapComponent,
    FavouritesComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: UserComponent},
      { path: ':id', component: UserDetailsComponent},
      { path: 'check/qrmap', component: QrmapComponent},
      { path: 'data/favourites', component: FavouritesComponent},
    ]),
    SharedModule,
    ZXingScannerModule
  ]
})
export class UserModule { }