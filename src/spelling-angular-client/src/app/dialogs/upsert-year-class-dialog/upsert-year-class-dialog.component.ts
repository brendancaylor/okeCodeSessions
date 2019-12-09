import { Component, OnInit, Inject } from '@angular/core';
import { YearClassViewmodel } from './year-class-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { YearClassUpdateDto, YearClassAddDto, YearClassClient } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-upsert-year-class-dialog',
  templateUrl: './upsert-year-class-dialog.component.html',
  styleUrls: ['./upsert-year-class-dialog.component.scss']
})
export class UpsertYearClassDialogComponent implements OnInit {

  yearClassViewmodel: YearClassViewmodel;
  constructor(public _dialogRef: MatDialogRef<UpsertYearClassDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: YearClassAddDto | YearClassUpdateDto,
      private fb: FormBuilder
      ) {
        debugger;
        this.yearClassViewmodel = new YearClassViewmodel(data, fb);
  }

  ngOnInit() {
  }

  cancel() {
    this._dialogRef.close();
  }

  edit() {
    const dto = this.yearClassViewmodel.getDto();
    this._dialogRef.close(dto);
  }

}
