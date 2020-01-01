import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageHomeworkComponent } from './manage-homework.component';
import { YearClassClient, HomeworkClient } from 'src/app/core/services/clients';
import { FormsModule } from '@angular/forms';
import {
  MatButtonModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatTableModule,
  MatToolbarModule,
  MatExpansionModule,
  MatDialog,
  MatListModule,
  MatSnackBarModule,
  MatSnackBar,
} from '@angular/material';

import { BrowserModule } from '@angular/platform-browser';
import { CoreModule } from 'src/app/core/core.module';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('ManageHomeworkComponent', () => {
  let component: ManageHomeworkComponent;
  let fixture: ComponentFixture<ManageHomeworkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        BrowserModule,
        FormsModule,
        MatFormFieldModule,
        MatDialogModule,
        MatButtonModule,
        MatToolbarModule,
        MatTableModule,
        MatInputModule,
        MatSelectModule,
        MatListModule,
        MatExpansionModule,
        MatSnackBarModule,
        CoreModule,
        RouterTestingModule
      ],
      declarations: [ ManageHomeworkComponent ],
      providers: [YearClassClient, HomeworkClient, MatDialog, MatSnackBar]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageHomeworkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
