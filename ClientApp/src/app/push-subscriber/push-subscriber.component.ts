import { Component, OnInit, Inject } from '@angular/core';
import { SwPush } from '@angular/service-worker';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-push-subscriber',
  templateUrl: './push-subscriber.component.html',
  styleUrls: ['./push-subscriber.component.css']
})
export class PushSubscriberComponent {
  private _subscription: PushSubscription;
  public operationName: string;
  constructor(private swPush: SwPush,
    private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
    swPush.subscription.subscribe((subscription) => {
      this._subscription = subscription;
      this.operationName = (this._subscription === null) ? 'Subscribe' : 'Unsubscribe';
    });
   };
  operation() {
    (this._subscription === null) ? this.subscribe() : this.unsubscribe(this._subscription.endpoint);
   };
   private subscribe() {
     // Retrieve public VAPID key from the server
    this.httpClient.get(this.baseUrl + 'api/PublicKey', { responseType: 'text' }).subscribe(publicKey => {
      // Request subscription with the service worker
      this.swPush.requestSubscription({
        serverPublicKey: publicKey
      })
      // Distribute subscription to the server
      .then(subscription => this.httpClient.post(this.baseUrl + 'api/PushSubscriptions', subscription, this.httpOptions).subscribe(
        () => { },
        error => console.error(error)
      ))
      .catch(error => console.error(error));
    },
    error => console.error(error));
  };
  
  private unsubscribe(endpoint) { 
    this.swPush.unsubscribe()
      .then(() => this.httpClient.delete(this.baseUrl + 'api/PushSubscriptions/' + encodeURIComponent(endpoint)).subscribe(() => { },
        error => console.error(error)
      ))
      .catch(error => console.error(error));
  }

  
  

}
