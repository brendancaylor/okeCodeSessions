import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CollegeYearclassComponent } from './college-yearclass.component';

describe('CollegeYearclassComponent', () => {
  let component: CollegeYearclassComponent;
  let fixture: ComponentFixture<CollegeYearclassComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollegeYearclassComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollegeYearclassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
