import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth-service';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  isLoggedIn = false;

  constructor(private _authService: AuthService,
    private router: Router) {
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

    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
          return;
      }
      window.scrollTo(0, 0);
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

  closeMenu(): void {
    document.getElementById('mySidebar').style.display = 'none';
    document.getElementById('myOverlay').style.display = 'none';
  }

  openMenu(): void {
    document.getElementById('mySidebar').style.display = 'block';
    document.getElementById('myOverlay').style.display = 'block';
  }
}
