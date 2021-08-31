import { Component, OnInit } from '@angular/core';
import { LoginManagerService } from 'src/infrastructure/auth/login-manager.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor(private loginManager: LoginManagerService) { }

  ngOnInit(): void {
  }

  get loggedIn():boolean {
    return this.loginManager.loggedIn;
  }
}
