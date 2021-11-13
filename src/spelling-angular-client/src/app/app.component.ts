import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth-service';
import { Router, NavigationEnd, NavigationStart, NavigationCancel, NavigationError, RouterEvent, Event } from '@angular/router';
import { LoadingService } from './core/services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  isLoggedIn = false;
  loading = true;
  constructor(
    private _authService: AuthService,
    private router: Router,
    public _loadingService: LoadingService) {

    router.events.pipe()
      .subscribe(
        (e) => {
          this.checkRouterEvent(e);
        }
      );

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
      (<any>window).ga('set', 'page', evt.urlAfterRedirects);
      (<any>window).ga('send', 'pageview');
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
    document.getElementById('mySidebar')!.style.display = 'none';
    document.getElementById('myOverlay')!.style.display = 'none';
  }

  openMenu(): void {
    document.getElementById('mySidebar')!.style.display = 'block';
    document.getElementById('myOverlay')!.style.display = 'block';
  }

  checkRouterEvent(routerEvent: Event): void {
    if (routerEvent instanceof NavigationStart) {
      this.loading = true;
    }

    if (routerEvent instanceof NavigationEnd ||
      routerEvent instanceof NavigationCancel ||
      routerEvent instanceof NavigationError) {
      this.loading = false;
    }
  }
}
