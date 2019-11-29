﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.1.6.0 (NJsonSchema v10.0.28.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { isMoment } from 'moment';
import * as moment from 'moment';
import * as qs from 'qs';
import { Constants } from 'src/app/constants';

export function buildQueryString(queryParams: any): string {
    const params = Object.entries(queryParams || {}).reduce((params: any, [key, value]) => {
        params[key] = convertQueryParam(value);
        return params;
    }, {});

    return qs.stringify(params, { addQueryPrefix: true, allowDots: true });
}

function convertQueryParam(queryParam: any): any {
    if (isMoment(queryParam)) {
        return queryParam.toISOString();
    } else if (Array.isArray(queryParam)) {
        const v = queryParam.map(convertQueryParam);
        return v;
    } else if (typeof queryParam === 'object' && queryParam != null) {
        return Object.entries(queryParam).reduce((queryParam: any, [key, value]) => {
            queryParam[key] = convertQueryParam(value);
            return queryParam;
        }, {});
    } else {
        return queryParam;
    }
}

export interface ISpeachClient {
    getSpeach(sentence?: string): Observable<Blob | null>;
}

@Injectable({
    providedIn: 'root'
})
export class SpeachClient implements ISpeachClient {
    constructor(private readonly httpClient: HttpClient) {
    }

    getSpeach(sentence?: string): Observable<Blob | null> {
        const queryString = buildQueryString({
            sentence,
        });
        const url = `${Constants.apiAutoGeneratedRoot}/api/Speach${queryString}`;

        return this.httpClient.get(url, { responseType: "blob" });
    }
}