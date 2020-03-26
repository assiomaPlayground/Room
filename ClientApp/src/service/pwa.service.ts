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
    }
  }
  unsubscribeToNotifications() {
    if(this.subscription != null){
      this.ExecUnsubscribe();
    }
  }

  private execSubscribe(){
    console.log("requesting")
    this.httpClient.get(this.baseUrl + 'api/Push/SubKey', { responseType: 'text' })
    .subscribe(publicKey => {
      this.swPush.requestSubscription({
        serverPublicKey:  publicKey
      })
      .then( subscription => {
        this.subscribed = true;
        this.httpClient.post<string>(this.baseUrl + 'api/Push', subscription)
        .subscribe(res => {
          this.alertService.info(res);
        })
      }).catch( e => console.log(e))
    })
  }

  private ExecUnsubscribe(){
    this.swPush.unsubscribe()
      .then(() => this.httpClient.delete(this.baseUrl + 'api/Push/' + encodeURIComponent(this.subscription.endpoint))
      .subscribe(() => { 
        this.subscribed = false;
        this.alertService.warn("Push notifications disabilitate"); 
      }))
  }
}