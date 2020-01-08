import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageCollegesComponent } from './manage-colleges/manage-colleges.component';
import { ClaimsRouteGuard } from '../core/claims-route-guard';
import { ManageHomeworkComponent } from './manage-homework/manage-homework.component';
import { CollegeYearclassComponent } from './college-yearclass/college-yearclass.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { UsageReportComponent } from './usage-report/usage-report.component';
import { ManageStandardListsComponent } from './manage-standard-lists/manage-standard-lists.component';
import {
  AddHomeWorkAssignmentsFromListComponent
} from './add-home-work-assignments-from-list/add-home-work-assignments-from-list.component';

const routes: Routes = [
  {
    path: 'manage-colleges',
    component: ManageCollegesComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterColleges'] }
  },
  {
    path: 'manage-standard-lists',
    component: ManageStandardListsComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterColleges'] }
  },
  {
    path: 'manage-users',
    component: ManageUsersComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterCollegeUsers'] }
  },
  {
    path: 'college-yearclass/:collegeId',
    component: CollegeYearclassComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterCollegeUsers'] }
  },
  {
    path: 'manage-homework',
    component: ManageHomeworkComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterHomework'] }
  },
  {
    path: 'homeWork-assignments-from-list/:yearClassId',
    component: AddHomeWorkAssignmentsFromListComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterHomework'] }
  },
  {
    path: 'usage-report',
    component: UsageReportComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterColleges'] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule { }
