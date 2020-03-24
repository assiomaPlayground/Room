import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './shareds/nav-menu/nav-menu.component';
import { getBaseUrl } from 'src/main';
import { AlertComponent } from './shareds/alert/alert.component';
import { AppRoutingModule, ModudeLayouts, RouteComponents } from './app.routing';
import { NgxQRCodeModule} from 'ngx-qrcode2';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { SharedModule } from './shareds/shared/shared.module';
import { IconMenuComponent } from './shareds/icon-menu/icon-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AlertComponent,
    IconMenuComponent,
    RouteComponents,
    ModudeLayouts
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    SharedModule,
    AppRoutingModule,
    NgxQRCodeModule,
  ],
  
providers: [
  {provide:'BASE_URL', useFactory : getBaseUrl},
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

  // provider used to create fake backend
 
],
  bootstrap: [AppComponent]
})
export class AppModule { }
