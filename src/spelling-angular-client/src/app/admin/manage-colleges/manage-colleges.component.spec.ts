import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageCollegesComponent } from './manage-colleges.component';

describe('ManageCollegesComponent', () => {
  let component: ManageCollegesComponent;
  let fixture: ComponentFixture<ManageCollegesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageCollegesComponent ]
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
