import { Routes, RouterModule } from '@angular/router';


import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ZXingScannerComponent } from './scanner/zxing-scanner.component';
import { ReservationComponent } from './reservation/reservation.component';



const appRoutes: Routes = [ 
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', redirectTo: '/login', pathMatch:'full' },
    { path: 'qrmap', component: ZXingScannerComponent},
    { path: 'reservation', component: ReservationComponent},
    { path: '**', redirectTo: '', pathMatch:'full' },
    { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)},
]   


export const routing = RouterModule.forRoot(appRoutes);