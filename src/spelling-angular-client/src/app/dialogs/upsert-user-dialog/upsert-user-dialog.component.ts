import { Component, OnInit, Inject } from '@angular/core';
import { UserViewmodel } from './user-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UpdateUserDto, AddUserDto } from 'src/app/core/services/clients';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-upsert-user-dialog',
  templateUrl: './upsert-user-dialog.component.html',
  styleUrls: ['./upsert-user-dialog.component.scss']
})
export class UpsertUserDialogComponent implements OnInit {

  userViewmodel: UserViewmodel;

  constructor(public _dialogRef: MatDialogRef<UpsertUserDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: AddUserDto | UpdateUserDto,
      private fb: FormBuilder) {
        this.userViewmodel = new UserViewmodel(data, fb);
  }

  ngOnInit() {
  }

}
