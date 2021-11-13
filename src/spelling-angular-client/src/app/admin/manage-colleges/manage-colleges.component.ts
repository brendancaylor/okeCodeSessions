import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CollegeClient, CollegeDto, NameOnlyUpsertDto } from 'src/app/core/services/clients';
import { Utils } from 'src/app/core/utils';
import { UpsertCollegeDialogComponent } from 'src/app/dialogs/upsert-college-dialog/upsert-college-dialog.component';
import * as moment from 'moment';

@Component({
  selector: 'app-manage-colleges',
  templateUrl: './manage-colleges.component.html',
  styleUrls: ['./manage-colleges.component.scss']
})
export class ManageCollegesComponent implements OnInit {
  displayedColumns = ['collegeName', 'actions'];
  dataSource = new MatTableDataSource<CollegeDto>();
  editedCollege: CollegeDto | null = null;
  error: string | null = null;

  constructor(
    private _collegeClient: CollegeClient,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.loadColleges();
  }

  loadColleges(): void {
    this._collegeClient.getAllColleges()
    .subscribe(
      (data) => {
        this.dataSource.data = data;
      }
    );
  }

  addCollege(): void {
    const collegeToAdd: CollegeDto = new CollegeDto();
    this.setupDialog(collegeToAdd);
  }

  editCollege(college: CollegeDto): void {
    this.editedCollege = college;
    const collegeToEdit: CollegeDto = new CollegeDto();
    Object.assign(collegeToEdit, college);
    this.setupDialog(collegeToEdit);
  }


  private setupDialog(collegeToEdit: CollegeDto): void {

    const dialogRef = this.dialog.open(UpsertCollegeDialogComponent, {
      width: '348px',
      data: collegeToEdit
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as CollegeDto;
      if (typedObject) {

        const dto: NameOnlyUpsertDto = new NameOnlyUpsertDto();
        dto.id = typedObject.id;
        dto.name = typedObject.collegeName;
        dto.rowVersion = typedObject.rowVersion;
        if (typedObject.id) {
          this.doUpdate(dto, typedObject);
        } else  {
          this.doAdd(dto);
        }
      }
    });
  }

  private doUpdate(dto: NameOnlyUpsertDto, college: CollegeDto): void {
    this._collegeClient.updateCollege(dto)
    .subscribe(
      (savedCollege) => {
        this.editedCollege!.collegeName = college.collegeName;
        this.editedCollege!.rowVersion = savedCollege.rowVersion;
       }
      , error => this.error = Utils.formatError(error)
    );
  }

  private doAdd(dto: NameOnlyUpsertDto): void {
    this._collegeClient.addCollege(dto)
        .subscribe(
          (savedCollege) => {
            const newCollege: CollegeDto = new CollegeDto({
                collegeName: dto.name,
                id: savedCollege.id,
                rowVersion: savedCollege.rowVersion,
                createdAt: moment(),  
                createdByAppUserId: ''
              }
            );

            let data = this.dataSource.data;
            data.push(newCollege);
            data = data.sort(Utils.compareCollegeName);
            this.dataSource.data = data;
          }
          , error => this.error = Utils.formatError(error)
        );
  }

}
