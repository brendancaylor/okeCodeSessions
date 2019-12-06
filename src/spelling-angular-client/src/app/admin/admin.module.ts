import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  MatButtonModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatTableModule,
  MatToolbarModule,
} from '@angular/material';

import { BrowserModule } from '@angular/platform-browser';
import { AdminRoutingModule } from './admin-routing.module';
import { DeleteDialogComponent } from './delete-dialog.component';
import { CoreModule } from '../core/core.module';
import { ManageCollegesComponent } from './manage-colleges/manage-colleges.component';
import { ManageHomeworkComponent } from './manage-homework/manage-homework.component';
import { CollegeYearclassComponent } from './college-yearclass/college-yearclass.component';
import { ManagUsersComponent } from './manag-users/manag-users.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    MatFormFieldModule,
    MatDialogModule,
    MatButtonModule,
    MatToolbarModule,
    MatTableModule,
    MatInputModule,
    MatSelectModule,
    AdminRoutingModule,
    CoreModule
  ],
  exports: [],
  declarations: [
    DeleteDialogComponent,
    ManageCollegesComponent,
    ManageHomeworkComponent,
    CollegeYearclassComponent,
    ManagUsersComponent
  ],
  providers: [],
  entryComponents: [
    DeleteDialogComponent,
  ]
})
export class AdminModule {}
