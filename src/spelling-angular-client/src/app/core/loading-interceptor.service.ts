import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpInterceptor,
    HttpResponse
} from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { LoadingService } from './services/loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    private totalRequests = 0;

    constructor(private loadingService: LoadingService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler) {
        this.totalRequests++;
        this.loadingService.isLoading.next(true);
        return next.handle(request).pipe(
            tap(res => {
                if (res instanceof HttpResponse) {
                    this.decreaseRequests();
                }
            }),
            catchError(err => {
                this.decreaseRequests();
                throw err;
            })
        );
    }

    private decreaseRequests() {
        this.totalRequests--;
        if (this.totalRequests === 0) {
            this.loadingService.isLoading.next(false);
        }
    }
}