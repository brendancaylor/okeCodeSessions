import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertHomeWorkAssignmentDialogComponent } from './upsert-home-work-assignment-dialog.component';
import { FormsModule, FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import {
  MatButtonModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatTableModule,
  MatToolbarModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material';

import { BrowserModule } from '@angular/platform-browser';
import { CoreModule } from 'src/app/core/core.module';
import { HomeWorkAssignmentClient, HomeWorkAssignmentUpdateDto } from 'src/app/core/services/clients';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UpsertHomeWorkAssignmentDialogComponent', () => {

  let component: UpsertHomeWorkAssignmentDialogComponent;
  let fixture: ComponentFixture<UpsertHomeWorkAssignmentDialogComponent>;
  const matDialogRefMock = jasmine.createSpy('MatDialogRef');
  const yearClass: HomeWorkAssignmentUpdateDto = new HomeWorkAssignmentUpdateDto({
    academicYear: 2019,
    collegeId: '',
      id: 'test1',
    }
  );

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        BrowserAnimationsModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatDialogModule,
        MatButtonModule,
        MatToolbarModule,
        MatTableModule,
        MatInputModule,
        MatSelectModule,
        CoreModule,
        RouterTestingModule
      ],
      declarations: [ UpsertHomeWorkAssignmentDialogComponent ],
      providers: [
        FormBuilder,
        HomeWorkAssignmentClient,
        { provide: MatDialogRef, useValue: { } },
        { provide: MAT_DIALOG_DATA, useValue: yearClass}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertHomeWorkAssignmentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
