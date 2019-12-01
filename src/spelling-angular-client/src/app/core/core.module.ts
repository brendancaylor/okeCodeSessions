import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './auth-interceptor.service';
import { AuthService } from './auth-service.component';
import { AccountService } from './account.service';
import { ProjectService } from './project.service';
import { AdminRouteGuard } from './admin-route-guard';
import { DateParserInterceptor } from './date-parser.interceptor';
import { ClaimsRouteGuard } from './claims-route-guard';

@NgModule({
    imports: [],
    exports: [],
    declarations: [],
    providers: [
        AuthService,
        AccountService,
        ProjectService,
        AdminRouteGuard,
        ClaimsRouteGuard,
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: DateParserInterceptor,
            multi: true
        }
    ],
})
export class CoreModule { }
