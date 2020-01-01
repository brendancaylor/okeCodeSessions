import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpsertStandardListItemComponent } from './upsert-standard-list-item.component';

describe('UpsertStandardListItemComponent', () => {
  let component: UpsertStandardListItemComponent;
  let fixture: ComponentFixture<UpsertStandardListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpsertStandardListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertStandardListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
