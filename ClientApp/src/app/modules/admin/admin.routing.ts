import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { NgModule } from "@angular/core";
import { QRCodeGenComponent } from "./qrcode-gen/qrcode-gen.component";

const adminRoutes: Routes = [
    {  path: 'qrcodegen', component: QRCodeGenComponent},
    { path: '', component: AdminComponent }
  ];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }