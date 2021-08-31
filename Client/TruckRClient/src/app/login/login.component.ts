import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { LoginManagerService } from 'src/infrastructure/auth/login-manager.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { LoaderService } from 'src/infrastructure/common/loader.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  focusLogin: boolean;
  focusPassword: boolean;

  loginForm: FormGroup;

  title: string = "logowanie";
  
  private loggedInSubscription: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private loginManager: LoginManagerService,
    private notifications: NotificationService,
    private router: Router,
    private loader: LoaderService 
    ) {
      this.loggedInSubscription = this.loginManager.loggedIn$.subscribe((state)=> {
        loader.hide();
        if(state.loggedIn) {
          this.notifications.showSuccess('Zalogowano!');
          this.router.navigate(['']);
        } else {
          this.notifications.showError('Logowanie nie powiodło się.');
        }
      });
    }
    
  ngOnInit(): void {
    this.initForm();
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("login-page");
  }

  ngOnDestroy():void {
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("login-page");

    this.loggedInSubscription.unsubscribe();
  }

  initForm = ():void => {
    this.loginForm = this.formBuilder.group({
      login: ["", [Validators.required]],
      password: ["", [Validators.required]]
    });
  }

  confirm = ():void => {
    this.loader.show("Logowanie...");
    this.loginManager.login(
      this.loginForm.controls["login"].value, 
      this.loginForm.controls["password"].value);
  }
}
