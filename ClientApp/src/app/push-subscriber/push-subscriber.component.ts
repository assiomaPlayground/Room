import { Component, OnInit, Inject } from '@angular/core';
import { PwaService } from 'src/service/pwa.service';

@Component({
  selector: 'app-push-subscriber',
  templateUrl: './push-subscriber.component.html',
  styleUrls: ['./push-subscriber.component.css']
})
export class PushSubscriberComponent {

  buttonText : string;

  constructor(private pwaservice : PwaService) {
    this.buttonText = this.pwaservice.subscribed ? "Disabilita Notifiche" : "Abilita notifiche";
  }

  operation() {
    if(!this.pwaservice.subscribed){
      this.pwaservice.subscribeToNotifications();
      this.buttonText = "Disabilita notifiche";
    }
    else{
      this.pwaservice.unsubscribeToNotifications();
      this.buttonText = "Abilita notifiche";
    }
  };
}