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
import { MyCollegesComponent } from './my-colleges/my-colleges.component';
import { ManageHomeworkComponent } from './manage-homework/manage-homework.component';

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
    MyCollegesComponent,
    ManageHomeworkComponent
  ],
  providers: [],
  entryComponents: [
    DeleteDialogComponent,
  ]
})
export class AdminModule {}
