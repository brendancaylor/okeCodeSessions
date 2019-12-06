import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  isLoggedIn = false;

  constructor(private _authService: AuthService) {
    this._authService.loginChanged.subscribe(loggedIn => {
      this.isLoggedIn = loggedIn;
      if (!this.isLoggedIn && this._authService.authContext) {
        this._authService.authContext.claims = [];
      }
    });
  }

  ngOnInit() {
    this._authService.isLoggedIn().then(loggedIn => {
      this.isLoggedIn = loggedIn;
    });
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }

  hasClaims(claims: string[]) {
    return this._authService.authContext && this._authService.authContext.hasClaims(...claims);
  }
}
