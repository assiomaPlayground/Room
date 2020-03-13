import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { AuthGuard } from "../guard/auth-guard";
import { UserComponent } from "../user/user.component";
import { NgModule } from "@angular/core";

const adminRoutes: Routes = [
    {
      path: 'admin', component: AdminComponent, /*canActivate: [AuthGuard],*/ children: [
        { path: 'users', component: UserComponent }
        
      ]
    }
  ];
  
  export const adminRouting = RouterModule.forChild(adminRoutes);