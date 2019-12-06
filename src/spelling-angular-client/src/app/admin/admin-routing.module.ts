import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageCollegesComponent } from './manage-colleges/manage-colleges.component';
import { ClaimsRouteGuard } from '../core/claims-route-guard';
import { ManageHomeworkComponent } from './manage-homework/manage-homework.component';
import { CollegeYearclassComponent } from './college-yearclass/college-yearclass.component';
import { ManagUsersComponent } from './manag-users/manag-users.component';

const routes: Routes = [

  {
    path: 'admin/manage-colleges',
    component: ManageCollegesComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterColleges'] }
  },
  {
    path: 'admin/manage-users',
    component: ManagUsersComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterAllUsers'] }
  },
  {
    path: 'admin/college-yearclass/:collegeId',
    component: CollegeYearclassComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterCollegeUsers'] }
  },
  {
    path: 'admin/manage-homework',
    component: ManageHomeworkComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterHomework'] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule { }
