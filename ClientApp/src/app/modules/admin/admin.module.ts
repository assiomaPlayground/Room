import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { FormsModule } from '@angular/forms';
import { AdminRoutingModule } from './admin.routing';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { QRCodeGenComponent } from './qrcode-gen/qrcode-gen.component';
import { SharedModule } from 'src/app/shareds/shared/shared.module';
import { BuildingComponent } from 'src/app/modules/admin/building/building.component';

@NgModule({
  declarations: [
    AdminComponent,
    QRCodeGenComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    NgxQRCodeModule,
    SharedModule,
    BuildingComponent
  ]
})
export class AdminModule { }
