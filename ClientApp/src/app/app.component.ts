import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/model/UserModel';
import { AuthenticationService } from 'src/service/authenticationservice.service';




@Component({ selector: 'app-root',
 templateUrl: './app.component.html' })
export class AppComponent {
    currentUser: UserModel;

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
