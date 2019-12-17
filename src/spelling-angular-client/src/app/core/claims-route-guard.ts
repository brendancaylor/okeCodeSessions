import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth-service';

@Injectable()
export class ClaimsRouteGuard implements CanActivate {
    constructor(private _authService: AuthService) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const isLoggedIn = await this._authService.isLoggedIn();
        if (!isLoggedIn) {
            return false;
        }
        const requiredClaims = route.data.requiredClaims || [];
        const isValid =  !!this._authService.authContext &&
        this._authService.authContext.hasClaims(...requiredClaims);
        return isValid;
    }
}
