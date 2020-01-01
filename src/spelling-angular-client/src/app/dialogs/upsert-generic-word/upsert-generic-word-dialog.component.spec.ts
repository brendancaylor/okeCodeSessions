import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertGenericWordDialogComponent } from './upsert-generic-word-dialog.component';
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

describe('UpsertGenericWordDialogComponent', () => {

  let component: UpsertGenericWordDialogComponent;
  let fixture: ComponentFixture<UpsertGenericWordDialogComponent>;
  const matDialogRefMock = jasmine.createSpy('MatDialogRef');
  const homeWorkAssignmentItemUpdateDto: HomeWorkAssignmentItemUpdateDto = new HomeWorkAssignmentItemUpdateDto({
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
      declarations: [ UpsertGenericWordDialogComponent ],
      providers: [
        FormBuilder,
        { provide: MatDialogRef, useValue: { } },
        { provide: MAT_DIALOG_DATA, useValue: homeWorkAssignmentItemUpdateDto}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertGenericWordDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
