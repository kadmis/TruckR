import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { ProgressbarModule } from "ngx-bootstrap/progressbar";
import { TooltipModule } from "ngx-bootstrap/tooltip";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { TabsModule } from "ngx-bootstrap/tabs";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { AlertModule } from "ngx-bootstrap/alert";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";
import { CarouselModule } from "ngx-bootstrap/carousel";
import { ModalModule, BsModalRef } from "ngx-bootstrap/modal";

import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from './shared/footer/footer.component';
import { DriverRegistrationComponent } from './registration/driver-registration/driver-registration.component';
import { DispatcherRegistrationComponent } from './registration/dispatcher-registration/dispatcher-registration.component';
import { JwtInterceptorService } from 'src/infrastructure/auth/jwt-interceptor.service';
import { LoginComponent } from './login/login.component';
import { CommonModule, DatePipe } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { PopoverModule } from "ngx-bootstrap/popover";
import { JwBootstrapSwitchNg2Module } from 'jw-bootstrap-switch-ng2';
import { MapComponent } from './dispatcher/map/map.component';
import { ToastrModule } from 'ngx-toastr';
import { RedirectComponent } from './redirect/redirect.component';
import { DriverDashboardComponent } from './driver/driver-dashboard/driver-dashboard.component';
import { ActivateComponent } from './registration/activate/activate/activate.component';
import { CreateAssignmentComponent } from './dispatcher/create-assignment/create-assignment.component';
import { SideBarComponent } from './dispatcher/map/side-bar/side-bar.component';
import { DatetimePipe } from 'src/infrastructure/pipes/datetime.pipe';

import {LOCALE_ID} from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePL from '@angular/common/locales/pl';

registerLocaleData(localePL);

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    DriverRegistrationComponent,
    DispatcherRegistrationComponent,
    LoginComponent,
    MapComponent,
    RedirectComponent,
    DriverDashboardComponent,
    ActivateComponent,
    CreateAssignmentComponent,
    SideBarComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    ProgressbarModule.forRoot(),
    TooltipModule.forRoot(),
    CollapseModule.forRoot(),
    PaginationModule.forRoot(),
    AlertModule.forRoot(),
    BsDatepickerModule.forRoot(),
    CarouselModule.forRoot(),
    CommonModule,
    BrowserModule,
    PopoverModule.forRoot(),
    JwBootstrapSwitchNg2Module,
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true
    },
    { provide: LOCALE_ID, useValue: 'pl' },
    DatePipe,
    BsModalRef
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule {}
