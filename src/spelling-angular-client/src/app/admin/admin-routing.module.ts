import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageCollegesComponent } from './manage-colleges/manage-colleges.component';
import { ClaimsRouteGuard } from '../core/claims-route-guard';
import { MyCollegesComponent } from './my-colleges/my-colleges.component';
import { ManageHomeworkComponent } from './manage-homework/manage-homework.component';

const routes: Routes = [

  {
    path: 'admin/manage-colleges',
    component: ManageCollegesComponent,
    canActivate: [ClaimsRouteGuard],
    data: { requiredClaims: ['AdminisiterColleges'] }
  },
  {
    path: 'admin/my-colleges',
    component: MyCollegesComponent,
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
