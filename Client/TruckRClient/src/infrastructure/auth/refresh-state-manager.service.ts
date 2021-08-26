import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RefreshTokenService } from '../methods/refresh-token/refresh-token.service';
import { RefreshTokenModel } from '../methods/refresh-token/refresh-token-model';
import { TokenManagerService } from './token-manager.service';
import { Subject, Observable } from 'rxjs';
import { UserManagerService } from './user-manager.service';

@Injectable({
  providedIn: 'root'
})
export class RefreshStateManagerService {
  private refreshIntervalId: any;

  private refreshSubject: Subject<boolean>;
  refresh$: Observable<boolean>;

  constructor(
    private refreshTokenService: RefreshTokenService,
    private tokenManagerService: TokenManagerService,
    private userManagerService: UserManagerService) {
      this.refreshSubject = new Subject<boolean>();
      this.refresh$ = this.refreshSubject.asObservable();
    }

  public set = (instant?: boolean):void => {
    if(instant) 
      this.refresh(this.tokenManagerService.refreshToken);

    this.refreshIntervalId = setInterval(()=> 
      this.refresh(this.tokenManagerService.refreshToken), (Number)(this.tokenManagerService.refreshInterval));
  }

  public clear = ():void => {
    if(this.refreshIntervalId) {
      clearInterval(this.refreshIntervalId);
    }
  }

  private refresh = (refreshToken: string):void => {
    let model = new RefreshTokenModel(refreshToken);
    this.refreshTokenService
      .refresh(model)
      .subscribe((res)=>{
        if(res.successful) {
          this.tokenManagerService.setApiToken(res.token);
          this.tokenManagerService.setRefreshToken(res.refreshToken, res.refreshInterval);
          this.clear();
          this.set();
        } else {
          this.tokenManagerService.clear();
          this.userManagerService.clear();
          this.clear();
        }
        this.refreshSubject.next(res.successful);
      });
  }
}
