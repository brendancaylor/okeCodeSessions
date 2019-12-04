import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './auth-interceptor.service';
import { AuthService } from './auth-service';
import { DateParserInterceptor } from './date-parser.interceptor';
import { ClaimsRouteGuard } from './claims-route-guard';

@NgModule({
    imports: [],
    exports: [],
    declarations: [],
    providers: [
        AuthService,
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
