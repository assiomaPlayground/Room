import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { AdminAuthGuard, AuthGuard } from 'src/app/guards/auth-guard'

import { LoginComponent } from './modules/main/login/login.component';
import { RegisterComponent } from './modules/main/register/register.component';
import { QrmapComponent } from './qrmap/qrmap.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { LayoutSelectorComponent } from './layouts/layout-selector/layout-selector.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

const appRoutes: Routes = [ 
    { path: 'home', canActivate: [AuthGuard], loadChildren: () => import('./modules/main/home/home.module').then(m => m.HomeModule) },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'admin', canActivate: [AdminAuthGuard], loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)},
    { path: 'room', canActivate: [AuthGuard], loadChildren: () => import('./modules/room/room.module').then(m => m.RoomModule)},
    { path: 'reservation', canActivate: [AuthGuard], loadChildren: () => import('./modules/reservation/reservation.module').then(m => m.ReservationModule) },
    { path: 'user', canActivate: [AuthGuard], loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule) },
    { path: 'qrmap', component: QrmapComponent},
    { path: '', redirectTo:   '/login', pathMatch:'full' },
    //{ path: '**', redirectTo: '/login', pathMatch:'full' }
]

export const RouteComponents : any[] = [
  QrmapComponent,
  LoginComponent,
  RegisterComponent,
];

export const ModudeLayouts : any[] = [
  LayoutSelectorComponent,
  MainLayoutComponent,
  AdminLayoutComponent
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }