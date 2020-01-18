import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs/';
import { HomeworkClient, HomeWorkAssignmentDto } from './clients';

@Injectable()
export class HomeworkResolverService implements Resolve<HomeWorkAssignmentDto> {

    constructor(
        private homeworkClient: HomeworkClient) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<HomeWorkAssignmentDto> {
        if (route.params['homeworkId']) {
            return this.homeworkClient.getHomeWorkAssignment(route.params['homeworkId']);
        } else {
            return null;
        }
    }
}
