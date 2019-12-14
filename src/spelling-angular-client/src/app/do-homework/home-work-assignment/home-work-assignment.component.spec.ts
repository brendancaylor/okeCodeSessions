import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeWorkAssignmentComponent } from './home-work-assignment.component';
import { HomeWorkAssignmentViewmodel, HomeworkItemViewmodel } from './home-work-assignment-viewmodel';
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
  MatIconModule,
} from '@angular/material';

import { BrowserModule } from '@angular/platform-browser';
import { CoreModule } from 'src/app/core/core.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { SpeachClient, HomeworkClient } from 'src/app/core/services/clients';
describe('HomeWorkAssignmentComponent', () => {
  let component: HomeWorkAssignmentComponent;
  let fixture: ComponentFixture<HomeWorkAssignmentComponent>;
  const fakeActivatedRoute = {
    data: null
  } as ActivatedRoute;

  const fakeRouter = {} as Router;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeWorkAssignmentComponent ],
      providers: [
        {provide: ActivatedRoute, useValue: fakeActivatedRoute},
        {provide: Router, useValue: fakeRouter},
        SpeachClient,
        HomeworkClient
      ],
      imports: [
        HttpClientTestingModule,
        BrowserAnimationsModule,
        BrowserModule,
        FormsModule,
        MatFormFieldModule,
        MatIconModule,
        MatDialogModule,
        MatButtonModule,
        MatToolbarModule,
        MatTableModule,
        MatInputModule,
        MatSelectModule,
        CoreModule,
        RouterTestingModule
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeWorkAssignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('word aBcDeFg with attempt afffffff should hint ab???f?', () => {
    component.viewmodel = new HomeWorkAssignmentViewmodel();
    const homeworkItem: HomeworkItemViewmodel = new HomeworkItemViewmodel();
    homeworkItem.word = 'aBcDeFg';
    homeworkItem.attempt = 'afffffff';
    component.viewmodel.homeworkItems.push(homeworkItem);
    expect(homeworkItem.hint).toEqual('ab???f?');
  });

  it('word aBcDeFg with attempt test should hint a??????', () => {
    component.viewmodel = new HomeWorkAssignmentViewmodel();
    const homeworkItem: HomeworkItemViewmodel = new HomeworkItemViewmodel();
    homeworkItem.word = 'aBcDeFg';
    homeworkItem.attempt = 'test';
    component.viewmodel.homeworkItems.push(homeworkItem);
    expect(homeworkItem.hint).toEqual('a??????');
  });

});
