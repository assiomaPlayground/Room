import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guard/auth-guard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';


const appRoutes: Routes = [ 
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', redirectTo: '/login', pathMatch:'full' },
    // otherwise redirect to home
    { path: '**', redirectTo: '', pathMatch:'full' }
];

export const routing = RouterModule.forRoot(appRoutes);