import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserComponent } from './user/user.component';
import { ReservationComponent } from './reservation/reservation.component';
import { BuildingComponent } from './building/building.component';
import { RoomComponent } from './room/room.component';
import { QrmapComponent } from './qrmap/qrmap.component';
import { FavouritesComponent } from './favourites/favourites.component';
import { getBaseUrl } from 'src/main';
import { UserModule } from './user/user.module';
import { RoomModule } from './room/room.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserComponent,
    ReservationComponent,
    BuildingComponent,
    RoomComponent,
    QrmapComponent,
    FavouritesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    UserModule,
    RoomModule,
    RouterModule.forRoot([

    ]),
    
  ],
  
providers: [
  {provide:'BASE_URL', useFactory : getBaseUrl}
],
  bootstrap: [AppComponent]
})
export class AppModule { }
