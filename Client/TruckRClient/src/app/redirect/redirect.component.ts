import { Component, OnInit } from '@angular/core';
import { UserManagerService } from 'src/infrastructure/auth/user-manager.service';
import { LoginManagerService } from 'src/infrastructure/auth/login-manager.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.scss']
})
export class RedirectComponent implements OnInit {

  constructor(
    userManager: UserManagerService,
    loginManager: LoginManagerService,
    router: Router) {
      console.log('Redirecting...');
      if(!loginManager.loggedIn)
        router.navigate(['login'])
      if(loginManager.loggedIn && userManager.isDispatcher)
        router.navigate(['dispatcher-dashboard']);
      if(loginManager.loggedIn && userManager.isDriver)
        router.navigate(['driver-dashboard']);
    }

  ngOnInit(): void {
  }
}
