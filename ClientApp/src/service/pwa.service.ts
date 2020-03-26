import { Injectable, Inject } from '@angular/core';
import { SwUpdate, SwPush, ServiceWorkerModule } from '@angular/service-worker';
import { HttpClient } from '@angular/common/http';
import { AlertService } from './alertservice.service';

@Injectable()
export class PwaService {

  private subscription : PushSubscription;
  private baseUrl: String;

  public subscribed : boolean;

  constructor(
    private swUpdate: SwUpdate, 
    private swPush: SwPush, 
    private swModule : ServiceWorkerModule,
    private httpClient : HttpClient,
    private alertService : AlertService,
    @Inject('BASE_URL') baseUrl : String
    ) 
    {
      this.baseUrl = baseUrl;
      swUpdate.available.subscribe(event => {
      if (event) {
        window.location.reload();
      }
    });
    swPush.subscription.subscribe((subscription) => {
      this.subscription = subscription;
    });
  }

  subscribeToNotifications() {
    if(this.subscription === null){
      this.execSubscribe();
      this.subscribed = true;
    }
  }
  unsubscribeToNotifications() {
    if(this.subscription != null){
      this.ExecUnsubscribe();
      this.subscribed = false;
    }
  }

  private execSubscribe(){
    this.httpClient.post(this.baseUrl + 'api/Push/SubKey', {})
    .subscribe(publicKey => {
      let pk = publicKey as string;
      console.log(pk);
      this.swPush.requestSubscription({
        serverPublicKey:  pk
      })
      .then( subscription => 
        this.httpClient.post(this.baseUrl + 'api/Push', subscription)
        .subscribe(res => {
          let message = res as string;
          this.alertService.info(message);
          console.log(message);
        })).catch( e => console.log(e))
    })
  }

  private ExecUnsubscribe(){
    console.log("us");
    this.swPush.unsubscribe()
      .then(() => this.httpClient.delete(this.baseUrl + 'api/Push/' + encodeURIComponent(this.subscription.endpoint))
      .subscribe(() => { this.alertService.warn("Push notifications disabilitate"); }))
  }
}