import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeWorkAssignmentComponent } from './home-work-assignment.component';

describe('HomeWorkAssignmentComponent', () => {
  let component: HomeWorkAssignmentComponent;
  let fixture: ComponentFixture<HomeWorkAssignmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeWorkAssignmentComponent ]
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
});
