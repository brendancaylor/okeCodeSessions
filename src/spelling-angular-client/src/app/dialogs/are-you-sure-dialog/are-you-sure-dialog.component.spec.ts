import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AreYouSureDialogComponent } from './are-you-sure-dialog.component';
import { MatDialogModule } from '@angular/material';
import { CoreModule } from 'src/app/core/core.module';
import { AppModule } from 'src/app/app.module';

describe('AreYouSureDialogComponent', () => {
  let component: AreYouSureDialogComponent;
  let fixture: ComponentFixture<AreYouSureDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AreYouSureDialogComponent ],
      imports: [
        MatDialogModule
      ],
      providers: []
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AreYouSureDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
