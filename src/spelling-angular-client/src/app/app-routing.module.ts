import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactUsComponent } from './home/contact-us.component';
import { HomeComponent } from './home/home.component';
import { SigninRedirectCallbackComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './home/signout-redirect-callback.component';
import { UnauthorizedComponent } from './home/unauthorized.component';
import { HomeWorkAssignmentComponent } from './do-homework/home-work-assignment/home-work-assignment.component';


const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'admin',
          loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
    },
    { path: 'contact-us', component: ContactUsComponent },
    { path: 'homework', component: HomeWorkAssignmentComponent },
    { path: 'homework/:homeworkId', component: HomeWorkAssignmentComponent },
    { path: 'signin-callback', component: SigninRedirectCallbackComponent },
    { path: 'signout-callback', component: SignoutRedirectCallbackComponent },
    { path: 'unauthorized', component: UnauthorizedComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
