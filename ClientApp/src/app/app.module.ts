import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './shareds/nav-menu/nav-menu.component';
import { FavouritesComponent } from './favourites/favourites.component';
import { getBaseUrl } from 'src/main';
import { AlertComponent } from './shareds/alert/alert.component';
import { AppRoutingModule, ModudeLayouts, RouteComponents } from './app.routing';
import { NgxQRCodeModule} from 'ngx-qrcode2';
import { ZXingScannerModule } from './scanner/zxing-scanner.module';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { SharedModule } from './shareds/shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FavouritesComponent,
    AlertComponent,
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
    ZXingScannerModule
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
