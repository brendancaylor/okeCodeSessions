import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatButtonModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatTableModule,
  MatToolbarModule,
  MatDatepickerModule,
  MAT_DATE_LOCALE
} from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminModule } from './admin/admin.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { ContactUsComponent } from './home/contact-us.component';
import { HomeComponent } from './home/home.component';
import { SigninRedirectCallbackComponent } from './home/signin-redirect-callback.component';
import { SignoutRedirectCallbackComponent } from './home/signout-redirect-callback.component';
import { UnauthorizedComponent } from './home/unauthorized.component';
import { HomeWorkAssignmentComponent } from './do-homework/home-work-assignment/home-work-assignment.component';
import { UpsertCollegeDialogComponent } from './dialogs/upsert-college-dialog/upsert-college-dialog.component';
import { UpsertUserDialogComponent } from './dialogs/upsert-user-dialog/upsert-user-dialog.component';
import { UpsertYearClassDialogComponent } from './dialogs/upsert-year-class-dialog/upsert-year-class-dialog.component';
import {
  UpsertHomeWorkAssignmentDialogComponent
} from './dialogs/upsert-home-work-assignment/upsert-home-work-assignment-dialog.component';
import {
  UpsertHomeWorkAssignmentItemDialogComponent
} from './dialogs/upsert-home-work-assignment-item/upsert-home-work-assignment-item-dialog.component';

import {MomentDateModule, MatMomentDateModule} from '@angular/material-moment-adapter';
import { HomeworkResolverService } from './core/services/homeork-resolver-service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ContactUsComponent,
    SigninRedirectCallbackComponent,
    SignoutRedirectCallbackComponent,
    UnauthorizedComponent,
    HomeWorkAssignmentComponent,
    UpsertCollegeDialogComponent,
    UpsertUserDialogComponent,
    UpsertYearClassDialogComponent,
    UpsertHomeWorkAssignmentDialogComponent,
    UpsertHomeWorkAssignmentItemDialogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    MatDialogModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    AdminModule,
    CoreModule,
    AppRoutingModule,
    MomentDateModule,
    MatDatepickerModule,
    MatMomentDateModule
  ],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB'},
    HomeworkResolverService
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    UpsertCollegeDialogComponent,
    UpsertUserDialogComponent,
    UpsertYearClassDialogComponent,
    UpsertHomeWorkAssignmentDialogComponent,
    UpsertHomeWorkAssignmentItemDialogComponent
  ]
})
export class AppModule { }
