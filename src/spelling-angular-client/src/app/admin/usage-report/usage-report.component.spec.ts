import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsageReportComponent } from './usage-report.component';
import { UserClient } from 'src/app/core/services/clients';

describe('UsageReportComponent', () => {
  let component: UsageReportComponent;
  let fixture: ComponentFixture<UsageReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsageReportComponent ],
      providers: [UserClient]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsageReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
