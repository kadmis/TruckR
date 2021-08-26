import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenManagerService } from './token-manager.service';

@Injectable({
    providedIn: 'root'
})
export class JwtInterceptorService implements HttpInterceptor {

    constructor(private tokenManager:TokenManagerService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.tokenManager.apiToken;
        
        if (token) {
            let clone = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${token}`
                }
            });

            return next.handle(clone);
        }

        return next.handle(req);
    }
}
