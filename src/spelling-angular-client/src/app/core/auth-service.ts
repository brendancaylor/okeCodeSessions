import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client';
import { Constants } from '../constants';
import { Subject } from 'rxjs';
import { AuthContext } from '../model/auth-context';
import { UserClient } from './services/clients';

@Injectable()
export class AuthService {
  private _userManager: UserManager;
  private _user: User;
  private _loginChangedSubject = new Subject<boolean>();

  loginChanged = this._loginChangedSubject.asObservable();
  authContext: AuthContext;

  constructor(private _userClient: UserClient) {

    const url = window.location.origin;
    const stsSettings = {
      authority: Constants.stsAuthority,
      client_id: Constants.clientId,
      redirect_uri: `${url}/signin-callback`,
      scope: 'openid profile spelling-api',
      response_type: 'code',
      post_logout_redirect_uri: `${url}/signout-callback`,
      automaticSilentRenew: true,
      silent_redirect_uri: `${url}/assets/silent-callback.html`
    };
    this._userManager = new UserManager(stsSettings);
    this._userManager.events.addAccessTokenExpired(_ => {
      this._loginChangedSubject.next(false);
    });
    this._userManager.events.addUserLoaded(user => {
      if (this._user !== user) {
        this._user = user;
        this.loadSecurityContext();
        this._loginChangedSubject.next(!!user && !user.expired);
      }
    });

  }

  login() {
    return this._userManager.signinRedirect();
  }

  isLoggedIn(): Promise<boolean> {
    return this._userManager.getUser().then(async user => {
      const userCurrent = !!user && !user.expired;
      if (this._user !== user) {
        this._loginChangedSubject.next(userCurrent);
      }
      if (userCurrent && !this.authContext) {
        await this.loadSecurityContext();
      }
      this._user = user;
      return userCurrent;
    });
  }

  completeLogin() {
    return this._userManager.signinRedirectCallback().then(user => {
      this._user = user;
      this._loginChangedSubject.next(!!user && !user.expired);
      return user;
    });
  }

  logout() {
    this._userManager.signoutRedirect();
  }

  completeLogout() {
    this._user = null;
    this._loginChangedSubject.next(false);
    return this._userManager.signoutRedirectCallback();
  }

  getAccessToken() {
    return this._userManager.getUser().then(user => {
      if (!!user && !user.expired) {
        return user.access_token;
      } else {
        return null;
      }
    });
  }

  async loadSecurityContext() {
    const result = await this._userClient.getCurrentUserClaims().toPromise();
    this.authContext = new AuthContext();
    this.authContext.claims = result.claims;
  }
}
