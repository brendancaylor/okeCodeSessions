import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagUsersComponent } from './manag-users.component';

describe('ManagUsersComponent', () => {
  let component: ManagUsersComponent;
  let fixture: ComponentFixture<ManagUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
