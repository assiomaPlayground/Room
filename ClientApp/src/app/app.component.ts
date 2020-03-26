import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/model/UserModel';
import { AuthenticationService } from 'src/service/authenticationservice.service';
import { PwaService } from 'src/service/pwa.service'

@Component({ selector: 'app-root',
 templateUrl: './app.component.html' })
export class AppComponent {
    currentUser: UserModel;
    installPwaEvent : any;
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private pwaService : PwaService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
        window.addEventListener('beforeinstallprompt', event => {
            this.installPwaEvent = event;
        });
    }

    installPwa(): void {
        this.installPwaEvent.prompt();
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}
