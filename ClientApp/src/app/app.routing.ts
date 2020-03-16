import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ZXingScannerComponent } from './scanner/zxing-scanner.component';
import { ReservationComponent } from './reservation/reservation.component';
import { HomeComponent } from './home/home.component';
<<<<<<< Updated upstream
import { ReservationsComponent } from './reservations/reservations.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
=======
import { PrenotazioniComponent } from './prenotazioni/prenotazioni.component';
import { QrmapComponent } from './qrmap/qrmap.component';
>>>>>>> Stashed changes

const appRoutes: Routes = [ 
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)},
    { path: 'qrmap', component: QrmapComponent},
    { path: 'reservation', component: ReservationComponent},
    { path: 'reservations', component: ReservationsComponent},
    {path:'reservationDetails', component: ReservationDetailsComponent},
    { path: '', redirectTo: '/login', pathMatch:'full' },
    //{ path: '**', redirectTo: '/login', pathMatch:'full' },
]   

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }