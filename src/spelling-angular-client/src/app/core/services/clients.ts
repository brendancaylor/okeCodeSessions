﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.1.6.0 (NJsonSchema v10.0.28.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
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

export interface ICollegeClient {
    getSomethigs(test?: string): Observable<CollegeDto[]>;
}

@Injectable({
    providedIn: 'root'
})
export class CollegeClient implements ICollegeClient {
    constructor(private readonly httpClient: HttpClient) {
    }

    getSomethigs(test?: string): Observable<CollegeDto[]> {
        const queryString = buildQueryString({
            test,
        });
        const url = `${Constants.apiAutoGeneratedRoot}/api/College${queryString}`;

        return this.httpClient.get<any>(url, {  })
            .pipe(map(result => result.map(CollegeDto.fromJS)))
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

export interface ITestClient {
    getSomethigs(test?: string): Observable<IdDto>;
}

@Injectable({
    providedIn: 'root'
})
export class TestClient implements ITestClient {
    constructor(private readonly httpClient: HttpClient) {
    }

    getSomethigs(test?: string): Observable<IdDto> {
        const queryString = buildQueryString({
            test,
        });
        const url = `${Constants.apiAutoGeneratedRoot}/api/Test${queryString}`;

        return this.httpClient.get<any>(url, {  })
            .pipe(map(IdDto.fromJS))
    }
}

export class BaseDto implements IBaseDto {
    id!: string;

    constructor(data?: IBaseDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
        }
    }

    static fromJS(data: any): BaseDto {
        data = typeof data === 'object' ? data : {};
        let result = new BaseDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        return data; 
    }

    clone(): BaseDto {
        const json = this.toJSON();
        let result = new BaseDto();
        result.init(json);
        return result;
    }
}

export interface IBaseDto {
    id: string;
}

export class CollegeDto extends BaseDto implements ICollegeDto {
    collegeName?: string | undefined;

    constructor(data?: ICollegeDto) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.collegeName = _data["collegeName"];
        }
    }

    static fromJS(data: any): CollegeDto {
        data = typeof data === 'object' ? data : {};
        let result = new CollegeDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["collegeName"] = this.collegeName;
        super.toJSON(data);
        return data; 
    }

    clone(): CollegeDto {
        const json = this.toJSON();
        let result = new CollegeDto();
        result.init(json);
        return result;
    }
}

export interface ICollegeDto extends IBaseDto {
    collegeName?: string | undefined;
}

export class IdDto implements IIdDto {
    id!: number;
    rowVersion?: string | undefined;

    constructor(data?: IIdDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.rowVersion = _data["rowVersion"];
        }
    }

    static fromJS(data: any): IdDto {
        data = typeof data === 'object' ? data : {};
        let result = new IdDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["rowVersion"] = this.rowVersion;
        return data; 
    }

    clone(): IdDto {
        const json = this.toJSON();
        let result = new IdDto();
        result.init(json);
        return result;
    }
}

export interface IIdDto {
    id: number;
    rowVersion?: string | undefined;
}