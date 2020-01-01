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
  MAT_DATE_LOCALE,
  MatIconModule
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
  UpsertGenericWordDialogComponent
} from './dialogs/upsert-generic-word/upsert-generic-word-dialog.component';

import {MomentDateModule, MatMomentDateModule} from '@angular/material-moment-adapter';
import { HomeworkResolverService } from './core/services/homeork-resolver-service';
import { PrivacyComponent } from './home/privacy/privacy.component';
import { WaitingDialogComponent } from './dialogs/waiting-dialog/waiting-dialog.component';
import { UpsertStandardListComponent } from './dialogs/upsert-standard-list/upsert-standard-list.component';
import { UpsertStandardListItemComponent } from './dialogs/upsert-standard-list-item/upsert-standard-list-item.component';

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
    UpsertGenericWordDialogComponent,
    PrivacyComponent,
    WaitingDialogComponent,
    UpsertStandardListComponent,
    UpsertStandardListItemComponent
  ],
  imports: [
    HttpClientModule,
    MatIconModule,
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
    UpsertGenericWordDialogComponent,
    UpsertStandardListComponent,
    UpsertStandardListItemComponent,
    WaitingDialogComponent
  ]
})
export class AppModule { }
