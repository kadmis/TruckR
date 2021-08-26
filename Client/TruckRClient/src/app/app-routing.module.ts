import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { Routes, RouterModule } from "@angular/router";

import { DriverRegistrationComponent } from './registration/driver-registration/driver-registration.component';
import { DispatcherRegistrationComponent } from './registration/dispatcher-registration/dispatcher-registration.component';
import { LoginComponent } from './login/login.component';
import { MapComponent } from './dispatcher/map/map.component';
import { DispatcherGuard } from 'src/infrastructure/guards/dispatcher.guard';
import { LoggedOutGuard } from 'src/infrastructure/guards/logged-out.guard';
import { RedirectComponent } from './redirect/redirect.component';
import { LoginGuard } from 'src/infrastructure/guards/login.guard';
import { AssignmentComponent } from './dispatcher/assignment/assignment.component';
import { ActivateComponent } from './registration/activate/activate/activate.component';
import { DriverDashboardComponent } from './driver/driver-dashboard/driver-dashboard.component';
import { DriverGuard } from 'src/infrastructure/guards/driver.guard';

const routes: Routes = [
  { 
    path: "", 
    component: RedirectComponent 
  },
  { 
    path: "map", 
    component: MapComponent, 
    canActivate: [DispatcherGuard, LoginGuard] 
  },
  { 
    path: "assignment", 
    component: AssignmentComponent, 
    canActivate: [DispatcherGuard, LoginGuard] 
  },
  { 
    path: "driver-dashboard", 
    component: DriverDashboardComponent, 
    canActivate: [DriverGuard, LoginGuard] 
  },
  { 
    path: "driver-registration", 
    component: DriverRegistrationComponent, 
    canActivate: [LoggedOutGuard] 
  },
  { 
    path: "dispatcher-registration", 
    component: DispatcherRegistrationComponent, 
    canActivate: [LoggedOutGuard] 
  },
  { 
    path: "activate/:userId/:activationId", 
    component: ActivateComponent, 
    canActivate: [LoggedOutGuard] 
  },
  { 
    path: "login", 
    component: LoginComponent, 
    canActivate: [LoggedOutGuard] 
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes, {
      useHash: true
    })
  ],
  exports: []
})
export class AppRoutingModule {}
