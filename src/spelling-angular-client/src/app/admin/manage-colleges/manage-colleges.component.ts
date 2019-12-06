import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { CollegeClient, CollegeDto, NameOnlyUpsertDto } from 'src/app/core/services/clients';
import { Utils } from 'src/app/core/utils';
import { UpsertCollegeDialogComponent } from 'src/app/dialogs/upsert-college-dialog/upsert-college-dialog.component';

@Component({
  selector: 'app-manage-colleges',
  templateUrl: './manage-colleges.component.html',
  styleUrls: ['./manage-colleges.component.scss']
})
export class ManageCollegesComponent implements OnInit {
  displayedColumns = ['collegeName', 'actions'];
  dataSource = new MatTableDataSource<CollegeDto>();
  //colleges: Array<CollegeDto>;
  editedCollege: CollegeDto;
  error: string;

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
        //this.colleges = data;
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
        this.editedCollege.collegeName = college.collegeName;
        this.editedCollege.rowVersion = savedCollege.rowVersion;
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
                createdAt: null,
                createdByAppUserId: null
              }
            );

            let data = this.dataSource.data;
            data.push(newCollege);
            data = data.sort(Utils.compareCollegeName);
            this.dataSource.data = data;
            //this.colleges.push(newCollege);
          }
          , error => this.error = Utils.formatError(error)
        );
  }

}
