import { Component, OnInit } from '@angular/core';
import { YearClassClient, LookupDto, YearClassDto, YearClassAddDto, YearClassUpdateDto } from 'src/app/core/services/clients';
import { Moment } from 'moment-timezone';
import * as moment from 'moment-timezone';
import { MatDialog } from '@angular/material';
import { UpsertYearClassDialogComponent } from 'src/app/dialogs/upsert-year-class-dialog/upsert-year-class-dialog.component';
import { Utils } from 'src/app/core/utils';

@Component({
  selector: 'app-manage-homework',
  templateUrl: './manage-homework.component.html',
  styleUrls: ['./manage-homework.component.scss']
})
export class ManageHomeworkComponent implements OnInit {

  collegeOptions: Array<LookupDto> = [];
  yearClassOptions: Array<YearClassDto> = [];
  selectedCollege: LookupDto;
  selectedYearClass: YearClassDto;
  editedYearClass: YearClassDto;

  constructor(
    private _yearClassClient: YearClassClient,
    public dialog: MatDialog) { }

  ngOnInit() {
    this._yearClassClient.getCollegeLookups()
    .subscribe(
      (data) => {
        this.collegeOptions = data;
        if (data.length === 1) {
          this.selectedCollege = data[0];
          this.collegeSelected();
        }
      }
    );
  }

  collegeSelected(): void {
    this.loadYearClasss();
  }

  loadYearClasss(): void {
    const year = Utils.getAcademicYear();
    this._yearClassClient.getYearClasses(year, this.selectedCollege.id)
      .subscribe(
        (data) => {
          this.yearClassOptions = data;
        }
      );
  }

  addYearClass(): void {
    const yearClassToAdd: YearClassAddDto = new YearClassAddDto();
    this.setupDialog(yearClassToAdd);
  }

  editYearClass(yearClass: YearClassDto): void {
    this.editedYearClass = yearClass;
    const yearClassToEdit: YearClassUpdateDto = new YearClassUpdateDto();
    Object.assign(yearClassToEdit, yearClass);
    this.setupDialog(yearClassToEdit);
  }


  private setupDialog(yearClassdto: YearClassAddDto | YearClassUpdateDto): void {
    yearClassdto.academicYear = Utils.getAcademicYear();
    yearClassdto.collegeId = this.selectedCollege.id;
    const dialogRef = this.dialog.open(UpsertYearClassDialogComponent, {
      width: '348px',
      data: yearClassdto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as YearClassAddDto | YearClassUpdateDto;
      if (typedObject) {
        if (typedObject instanceof YearClassAddDto) {
          this.doAdd(typedObject);
        } else if (typedObject instanceof YearClassUpdateDto) {
          this.doUpdate(typedObject);
        }
      }
    });
  }

  private doUpdate(dto: YearClassUpdateDto): void {
    this._yearClassClient.updateYearClass(dto)
    .subscribe(
      (savedYearClass) => {
        this.loadYearClasss();
       }
    );
  }

  private doAdd(dto: YearClassAddDto): void {
    this._yearClassClient.addYearClass(dto)
        .subscribe(
          (savedYearClass) => {
            this.loadYearClasss();
          }
        );
  }

  yearClassSelected(): void {
  }

}
