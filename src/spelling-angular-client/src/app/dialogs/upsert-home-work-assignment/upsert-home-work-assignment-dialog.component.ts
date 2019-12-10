import { Component, OnInit, Inject } from '@angular/core';
import { HomeWorkAssignmentViewmodel } from './home-work-assignment-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { HomeWorkAssignmentUpdateDto, HomeWorkAssignmentAddDto } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-upsert-home-work-assignment-dialog',
  templateUrl: './upsert-home-work-assignment-dialog.component.html',
  styleUrls: ['./upsert-home-work-assignment-dialog.component.scss']
})
export class UpsertHomeWorkAssignmentDialogComponent implements OnInit {

  homeWorkAssignmentItemViewmodel: HomeWorkAssignmentViewmodel;
  constructor(public _dialogRef: MatDialogRef<UpsertHomeWorkAssignmentDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto,
      private fb: FormBuilder
      ) {
        this.homeWorkAssignmentItemViewmodel = new HomeWorkAssignmentViewmodel(data, fb);
  }

  ngOnInit() {
  }

  cancel() {
    this._dialogRef.close();
  }

  edit() {
    const dto = this.homeWorkAssignmentItemViewmodel.getDto();
    this._dialogRef.close(dto);
  }

}
