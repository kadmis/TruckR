/*
=========================================================
* BLK Design System Angular - v1.2.0
=========================================================

* Product Page: https://www.creative-tim.com/product/blk-design-system-angular
* Copyright 2021 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 */

import { enableProdMode } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { AppModule } from "./app/app.module";
import { environment } from "./environments/environment";

if (environment.production) {
  enableProdMode();
}

export function authServiceUrl() {
  return environment.authServiceRoot;
}

export function locationsHubUrl() {
  return environment.locationsHub;
}

export function transportServiceUrl() {
  return environment.transportServiceRoot;
}

export function transportHubUrl() {
  return environment.transportHub;
}

const providers = [
{provide: "AUTH_SERVICE_URL", useFactory: authServiceUrl, deps:[]},
{provide: "LOCATIONS_HUB_URL", useFactory: locationsHubUrl, deps:[]},
{provide: "TRANSPORT_SERVICE_URL", useFactory: transportServiceUrl, deps:[]},
{provide: "TRANSPORT_HUB_URL", useFactory: transportHubUrl, deps:[]}
];

platformBrowserDynamic(providers)
  .bootstrapModule(AppModule)
  .catch(err => console.error(err));
