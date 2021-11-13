import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { from, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Constants } from '../constants';
import { AuthService } from './auth-service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private _authService: AuthService,
    private _router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //return next.handle(req);
    
    if (req.url.startsWith(Constants.apiRoot)) {
      
      const result = from(this._authService.getAccessToken().then(token => {

        if (!token) {
          return next.handle(req)

          .pipe(tap(_ => { }, error => {
            const respError = error as HttpErrorResponse;
            if (respError && (respError.status === 401 || respError.status === 403)) {
              this._router.navigate(['/unauthorized']);
            }
          }))

          .toPromise();
        }

        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        const authReq = req.clone({ headers });
        return next.handle(authReq)
          .pipe(tap(_ => { }, error => {
            const respError = error as HttpErrorResponse;
            if (respError && (respError.status === 401 || respError.status === 403)) {
              this._router.navigate(['/unauthorized']);
            }
          }
        ))
        .toPromise();
      }));

      if(result === undefined) {
        return next.handle(req);
      } else {
        return result as Observable<HttpEvent<any>>;
      }
      
    } else {
      return next.handle(req);
    }
    
  }
}
