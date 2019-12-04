import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageHomeworkComponent } from './manage-homework.component';

describe('ManageHomeworkComponent', () => {
  let component: ManageHomeworkComponent;
  let fixture: ComponentFixture<ManageHomeworkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageHomeworkComponent ]
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
