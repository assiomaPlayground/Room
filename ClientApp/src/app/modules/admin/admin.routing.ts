import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { NgModule } from "@angular/core";
import { QRCodeGenComponent } from "./qrcode-gen/qrcode-gen.component";
import { UsersComponent } from "./users/users.component";

const adminRoutes: Routes = [
    {  path: '', component: AdminComponent },
    {  path: 'qrcodegen', component: QRCodeGenComponent},
    {  path: 'users', component:UsersComponent}
  ];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }