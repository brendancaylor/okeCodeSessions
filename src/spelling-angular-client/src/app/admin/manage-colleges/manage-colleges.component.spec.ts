import { async, ComponentFixture, TestBed } from '@angular/core/testing';

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
  MatDialog,
} from '@angular/material';

import { BrowserModule } from '@angular/platform-browser';
import { ManageCollegesComponent } from './manage-colleges.component';
import { CoreModule } from 'src/app/core/core.module';
import { CollegeClient } from 'src/app/core/services/clients';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';

describe('ManageCollegesComponent', () => {
  let component: ManageCollegesComponent;
  let fixture: ComponentFixture<ManageCollegesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
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
        CoreModule,
        RouterTestingModule
      ],
      declarations: [ ManageCollegesComponent ],
      providers: [CollegeClient, MatDialog]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageCollegesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
