import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/model/user';
import { AuthenticationService } from 'src/service/authenticationservice.service';




@Component({ selector: 'app-app',
 templateUrl: './app.component.html' })
export class AppComponent {
    currentUser: User;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}
