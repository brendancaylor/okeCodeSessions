import { Injectable } from '@angular/core';
import {
    HttpHandler,
    HttpHeaderResponse,
    HttpInterceptor,
    HttpProgressEvent,
    HttpRequest,
    HttpResponse,
    HttpSentEvent,
    HttpUserEvent,
    HttpEventType
} from '@angular/common/http';
import { Moment } from 'moment-timezone';
import * as moment from 'moment-timezone';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class DateParserInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler):
        Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
        return next.handle(request)
            .pipe(map(response => {
                if (response.type !== HttpEventType.Response || !response.ok) {
                    return response;
                }

                return response.clone({ body: this.parseDates(response.body) });
            }));
    }

    private parseDates(body: any): any {
        if (body == null || typeof body !== 'object') {
            return body;
        }

        for (const [key, value] of Object.entries(body)) {
            if (typeof value === 'string') {
                body[key] = this.parseDate(value);
            } else if (typeof value === 'object') {
                body[key] = this.parseDates(value);
            }
        }

        return body;
    }

    private parseDate(value: string): string | Moment {
        let date = moment(value, ['YYYY-MM-DDTHH:mm:ssZZ', 'YYYY-MM-DDTHH:mm:ss.SSSSZZ'], true);

        if (date.isValid()) {
            return date;
        }

        date = moment(value, 'YYYY-MM-DDTHH:mm:ss', true);

        if (date.isValid()) {
            return date;
        }

        return value;
    }
}
