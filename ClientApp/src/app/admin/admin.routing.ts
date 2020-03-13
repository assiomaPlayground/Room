import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { AuthGuard } from "../guard/auth-guard";
import { NgModule } from "@angular/core";

const adminRoutes: Routes = [
    { path: '', component: AdminComponent, /*canActivate: [AuthGuard],*/ }
  ];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }