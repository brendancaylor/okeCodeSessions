import { Component, OnInit, Inject } from '@angular/core';
import { HomeWorkAssignmentItemViewmodel } from './home-work-assignment-item-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { HomeWorkAssignmentItemUpdateDto, HomeWorkAssignmentItemAddDto } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';
import { Utils } from 'src/app/core/utils';

@Component({
  selector: 'app-upsert-home-work-assignment-dialog',
  templateUrl: './upsert-home-work-assignment-item-dialog.component.html',
  styleUrls: ['./upsert-home-work-assignment-item-dialog.component.scss']
})
export class UpsertHomeWorkAssignmentItemDialogComponent implements OnInit {

  homeWorkAssignmentItemViewmodel: HomeWorkAssignmentItemViewmodel;
  languages = Utils.getLanguages();

  constructor(public _dialogRef: MatDialogRef<UpsertHomeWorkAssignmentItemDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto,
      private fb: FormBuilder
      ) {
        this.homeWorkAssignmentItemViewmodel = new HomeWorkAssignmentItemViewmodel(data, fb);
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
