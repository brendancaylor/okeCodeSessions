import { Component, OnInit, Inject } from '@angular/core';
import { UpsertStandardListViewmodel } from './upsert-standard-list-viewmodel';
import { Utils } from 'src/app/core/utils';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StandardListDto } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-upsert-standard-list',
  templateUrl: './upsert-standard-list.component.html',
  styleUrls: ['./upsert-standard-list.component.scss']
})
export class UpsertStandardListComponent implements OnInit {

  standardListViewmodel: UpsertStandardListViewmodel;
  languages = Utils.getLanguages();

  constructor(public _dialogRef: MatDialogRef<UpsertStandardListComponent>,
      @Inject(MAT_DIALOG_DATA) public data: StandardListDto,
      private fb: FormBuilder
      ) {
        this.standardListViewmodel = new UpsertStandardListViewmodel(data, fb);
  }

  ngOnInit() {
  }

  cancel() {
    this._dialogRef.close();
  }

  edit() {
    const dto = this.standardListViewmodel.getDto();
    this._dialogRef.close(dto);
  }

}
