import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertHomeWorkAssignmentItemDialogComponent } from './upsert-home-work-assignment-item-dialog.component';
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
import { HomeWorkAssignmentItemUpdateDto } from 'src/app/core/services/clients';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UpsertHomeWorkAssignmentItemDialogComponent', () => {

  let component: UpsertHomeWorkAssignmentItemDialogComponent;
  let fixture: ComponentFixture<UpsertHomeWorkAssignmentItemDialogComponent>;
  const matDialogRefMock = jasmine.createSpy('MatDialogRef');
  const yearClass: HomeWorkAssignmentItemUpdateDto = new HomeWorkAssignmentItemUpdateDto({
    homeWorkAssignmentId: '',
    id: 'test1'
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
      declarations: [ UpsertHomeWorkAssignmentItemDialogComponent ],
      providers: [
        FormBuilder,
        { provide: MatDialogRef, useValue: { } },
        { provide: MAT_DIALOG_DATA, useValue: yearClass}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertHomeWorkAssignmentItemDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
