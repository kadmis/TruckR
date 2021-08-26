import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ActivationService } from 'src/infrastructure/methods/activate/activation.service';
import { NotificationService } from 'src/infrastructure/common/notification.service';

@Component({
  selector: 'app-activate',
  templateUrl: './activate.component.html',
  styleUrls: ['./activate.component.scss']
})
export class ActivateComponent implements OnInit {

  constructor(
    private activateService: ActivationService,
    private notificationService: NotificationService,
    private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit(): void {
    this.activate();
  }

  private activate = ():void => {
    this.activateService.activate(this.userIdFromRoute, this.activationIdFromRoute).subscribe((res)=>{
      if(res.successful) {
        this.notificationService.showSuccess("Aktywowano konto! Możesz się zalogować.");
      } else {
        this.notificationService.showError("Aktywacja konta nie powiodła się.");
      }
      this.router.navigate(['']);
    });
  };

  private get userIdFromRoute():string {
    return this.route && this.route.snapshot.params["userId"];
  };

  private get activationIdFromRoute():string {
    return this.route && this.route.snapshot.params["activationId"];
  };
}
