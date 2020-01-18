import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactUsComponent } from './home/contact-us.component';
import { HomeComponent } from './home/home.component';
import { SigninRedirectCallbackComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './home/signout-redirect-callback.component';
import { UnauthorizedComponent } from './home/unauthorized.component';
import { HomeWorkAssignmentComponent } from './do-homework/home-work-assignment/home-work-assignment.component';
import { HomeworkResolverService } from './core/services/homework-resolver-service';
import { PrivacyComponent } from './home/privacy/privacy.component';


const routes: Routes = [
    { path: 'admin',
          loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
    },
    { path: 'contact-us', component: ContactUsComponent },
    { path: 'privacy', component: PrivacyComponent },
    { path: 'homework', component: HomeWorkAssignmentComponent },
    { path: 'homework/:homeworkId', component: HomeWorkAssignmentComponent, resolve: {homeworkData: HomeworkResolverService} },
    { path: 'signin-callback', component: SigninRedirectCallbackComponent },
    { path: 'signout-callback', component: SignoutRedirectCallbackComponent },
    { path: 'unauthorized', component: UnauthorizedComponent },
    { path: '',
    pathMatch: 'full',
    component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
