import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth-service.component';

@Injectable()
export class ClaimsRouteGuard implements CanActivate {
    constructor(private _authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
        const requiredClaims = route.data.requiredClaims || [];
        return !!this._authService.authContext &&
        this._authService.authContext.hasClaims(...requiredClaims);
    }
}