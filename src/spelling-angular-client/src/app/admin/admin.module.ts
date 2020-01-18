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
  MatListModule,
  MatDatepickerModule,
  MAT_DATE_LOCALE,
  MatExpansionModule,
  MatSnackBarModule,
  MatMenuModule,
  MatButtonToggleModule,
  MatIconModule,
  MatBadgeModule
} from '@angular/material';

import { BrowserModule } from '@angular/platform-browser';
import { AdminRoutingModule } from './admin-routing.module';
import { DeleteDialogComponent } from './delete-dialog.component';
import { CoreModule } from '../core/core.module';
import { ManageCollegesComponent } from './manage-colleges/manage-colleges.component';
import { ManageHomeworkComponent } from './manage-homework/manage-homework.component';
import { CollegeYearclassComponent } from './college-yearclass/college-yearclass.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { UsageReportComponent } from './usage-report/usage-report.component';
import { CommonModule } from '@angular/common';
import { ManageStandardListsComponent } from './manage-standard-lists/manage-standard-lists.component';
import {
  AddHomeWorkAssignmentsFromListComponent
} from './add-home-work-assignments-from-list/add-home-work-assignments-from-list.component';
import {MomentDateModule, MatMomentDateModule} from '@angular/material-moment-adapter';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatToolbarModule,
    MatTableModule,
    MatInputModule,
    MatSelectModule,
    MatListModule,
    AdminRoutingModule,
    MomentDateModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatExpansionModule,
    MatSnackBarModule,
    MatMenuModule,
    MatButtonToggleModule,
    MatIconModule,
    MatBadgeModule,
    CoreModule
  ],
  exports: [],
  declarations: [
    DeleteDialogComponent,
    ManageCollegesComponent,
    ManageHomeworkComponent,
    CollegeYearclassComponent,
    ManageUsersComponent,
    UsageReportComponent,
    ManageStandardListsComponent,
    AddHomeWorkAssignmentsFromListComponent
  ],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB'},
  ],
  entryComponents: [
    DeleteDialogComponent,
  ]
})
export class AdminModule {}
