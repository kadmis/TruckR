import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ActivationService } from 'src/infrastructure/methods/activate/activation.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';
import { LoginManagerService } from 'src/infrastructure/auth/login-manager.service';
import { Subscription } from 'rxjs';
import { LogoutPipelineService } from 'src/infrastructure/common/logout-pipeline.service';

@Component({
  selector: 'app-activate',
  templateUrl: './activate.component.html',
  styleUrls: ['./activate.component.scss']
})
export class ActivateComponent implements OnInit, OnDestroy {

  private loggedoutSub: Subscription;

  constructor(
    private activateService: ActivationService,
    private notificationService: NotificationService,
    private route: ActivatedRoute,
    private router: Router,
    private loginManager: LoginManagerService,
    private logoutPipeline: LogoutPipelineService) {
      this.loggedoutSub =
      this.loginManager.loggedOut$.subscribe(()=> {
        this.activate();
      })
    }

  ngOnInit(): void {
    this.logoutPipeline.logout(false);
  }

  ngOnDestroy():void {
    this.loggedoutSub.unsubscribe();
  }

  private activate = ():void => {
    this.activateService.activate(this.userId, this.activationId).subscribe((res)=>{
      if(res.successful) {
        this.notificationService.showSuccess("Aktywowano konto! Możesz się zalogować.");
      } else {
        this.notificationService.showError("Aktywacja konta nie powiodła się.");
      }
      this.router.navigate(['']);
    });
  };

  private get userId():string {
    return this.route && this.route.snapshot.params["userId"];
  };

  private get activationId():string {
    return this.route && this.route.snapshot.params["activationId"];
  };
}
