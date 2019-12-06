import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertCollegeDialogComponent } from './upsert-college-dialog.component';
import { FormsModule } from '@angular/forms';
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
import { CollegeClient, CollegeDto } from 'src/app/core/services/clients';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UpsertCollegeDialogComponent', () => {

  let component: UpsertCollegeDialogComponent;
  let fixture: ComponentFixture<UpsertCollegeDialogComponent>;
  const matDialogRefMock = jasmine.createSpy('MatDialogRef');
  const college: CollegeDto = new CollegeDto({
      collegeName: 'Test',
      createdAt: null,
      createdByAppUserId: null,
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
      declarations: [ UpsertCollegeDialogComponent ],
      providers: [
        CollegeClient,
        { provide: MatDialogRef, useValue: { } },
        { provide: MAT_DIALOG_DATA, useValue: college}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertCollegeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
