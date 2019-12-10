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
  MatToolbarModule
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
import { UpsertHomeWorkAssignmentComponent } from './dialogs/upsert-home-work-assignment/upsert-home-work-assignment-dialog.component';
import { UpsertHomeWorkAssignmentItemComponent } from './dialogs/upsert-home-work-assignment-item/upsert-home-work-assignment-item-dialog.component';

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
    UpsertHomeWorkAssignmentComponent,
    UpsertHomeWorkAssignmentItemComponent
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
    AppRoutingModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    UpsertCollegeDialogComponent,
    UpsertUserDialogComponent,
    UpsertYearClassDialogComponent
  ]
})
export class AppModule { }
