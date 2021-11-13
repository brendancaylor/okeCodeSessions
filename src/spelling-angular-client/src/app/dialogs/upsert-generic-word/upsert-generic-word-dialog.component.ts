import { Component, OnInit, Inject } from '@angular/core';
import { UpsertGenericWordViewmodel } from './upsert-generic-word-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HomeWorkAssignmentItemUpdateDto, HomeWorkAssignmentItemAddDto, StandardListItemDto } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';
import { Utils } from 'src/app/core/utils';

@Component({
  selector: 'app-upsert-generic-word-dialog',
  templateUrl: './upsert-generic-word-dialog.component.html',
  styleUrls: ['./upsert-generic-word-dialog.component.scss']
})

export class UpsertGenericWordDialogComponent implements OnInit {

  upsertGenericWordViewmodel: UpsertGenericWordViewmodel;
  languages = Utils.getLanguages();

  constructor(public _dialogRef: MatDialogRef<UpsertGenericWordDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: StandardListItemDto | HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto,
      private fb: FormBuilder
      ) {
        this.upsertGenericWordViewmodel = new UpsertGenericWordViewmodel(data, fb);
  }

  ngOnInit() {
  }

  cancel() {
    this._dialogRef.close();
  }

  edit() {
    const dto = this.upsertGenericWordViewmodel.getDto();
    this._dialogRef.close(dto);
  }

}
