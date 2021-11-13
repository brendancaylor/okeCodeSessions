import { Component, OnInit, Inject } from '@angular/core';
import { HomeWorkAssignmentViewmodel } from './home-work-assignment-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HomeWorkAssignmentUpdateDto, HomeWorkAssignmentAddDto } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';
import * as moment from 'moment';

@Component({
  selector: 'app-upsert-home-work-assignment-dialog',
  templateUrl: './upsert-home-work-assignment-dialog.component.html',
  styleUrls: ['./upsert-home-work-assignment-dialog.component.scss']
})
export class UpsertHomeWorkAssignmentDialogComponent implements OnInit {
  homeWorkAssignmentViewmodel: HomeWorkAssignmentViewmodel;
  constructor(public _dialogRef: MatDialogRef<UpsertHomeWorkAssignmentDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto,
      private fb: FormBuilder
      ) {
        this.homeWorkAssignmentViewmodel = new HomeWorkAssignmentViewmodel(data, fb);
  }

  ngOnInit(): void {
  }

  cancel(): void {
    this._dialogRef.close();
  }

  edit(): void {
    const dto = this.homeWorkAssignmentViewmodel.getDto();
    this._dialogRef.close(dto);
  }

}
