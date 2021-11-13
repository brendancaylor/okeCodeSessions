import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CollegeDto } from 'src/app/core/services/clients';
import { UpsertMode } from 'src/app/core/upsert-modes';

@Component({
  selector: 'app-upsert-college-dialog',
  templateUrl: './upsert-college-dialog.component.html',
  styleUrls: ['./upsert-college-dialog.component.scss']
})
export class UpsertCollegeDialogComponent implements OnInit {

  upsertModeEnum = UpsertMode;
  upsertMode: UpsertMode = UpsertMode.Add;
  college: CollegeDto;
  error: string | null = null;
  constructor(public _dialogRef: MatDialogRef<UpsertCollegeDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: CollegeDto) {
    this.college = data;
    this.upsertMode = this.college.id ? this.upsertModeEnum.Edit : this.upsertModeEnum.Add;
  }

  ngOnInit() { }

  cancel() {
      this._dialogRef.close();
  }

  edit() {
      if (this.college.collegeName) {
        this._dialogRef.close(this.college);
      } else {
        this.error = 'Please enter a name.';
      }
  }
}
