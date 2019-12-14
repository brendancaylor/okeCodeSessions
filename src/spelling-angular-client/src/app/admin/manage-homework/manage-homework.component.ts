import { Component, OnInit } from '@angular/core';
import {
  YearClassClient,
  LookupDto,
  YearClassDto,
  YearClassAddDto,
  YearClassUpdateDto,
  HomeworkClient,
  HomeWorkAssignmentDto,
  HomeWorkAssignmentAddDto,
  HomeWorkAssignmentUpdateDto,
  HomeWorkAssignmentItemDto,
  HomeWorkAssignmentItemAddDto,
  HomeWorkAssignmentItemUpdateDto
} from 'src/app/core/services/clients';
import { Moment } from 'moment-timezone';
import * as moment from 'moment-timezone';
import { UpsertYearClassDialogComponent } from 'src/app/dialogs/upsert-year-class-dialog/upsert-year-class-dialog.component';
import { Utils } from 'src/app/core/utils';
import { MatTableDataSource, MatDialog } from '@angular/material';
import {
  UpsertHomeWorkAssignmentDialogComponent
} from 'src/app/dialogs/upsert-home-work-assignment/upsert-home-work-assignment-dialog.component';
import {
  UpsertHomeWorkAssignmentItemDialogComponent
} from 'src/app/dialogs/upsert-home-work-assignment-item/upsert-home-work-assignment-item-dialog.component';

@Component({
  selector: 'app-manage-homework',
  templateUrl: './manage-homework.component.html',
  styleUrls: ['./manage-homework.component.scss']
})
export class ManageHomeworkComponent implements OnInit {

  collegeOptions: Array<LookupDto> = [];
  selectedCollege: LookupDto;

  yearClassOptions: Array<YearClassDto> = [];
  selectedYearClass: YearClassDto;
  editedYearClass: YearClassDto;

  homeWorkAssignments: Array<HomeWorkAssignmentDto> = [];
  editedHomeWorkAssignment: HomeWorkAssignmentDto;
  selectedHomeWorkAssignment: HomeWorkAssignmentDto;

  editedHomeWorkAssignmentItem: HomeWorkAssignmentItemDto;
  selectedHomeWorkAssignmentItem: HomeWorkAssignmentItemDto;


  showWords = false;
  showSubmittedWork = false;

  constructor(
    private _yearClassClient: YearClassClient,
    private _homeworkClient: HomeworkClient,
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
    this.setupYearClassDialog(yearClassToAdd);
  }

  editYearClass(yearClass: YearClassDto): void {
    this.editedYearClass = yearClass;
    const yearClassToEdit: YearClassUpdateDto = new YearClassUpdateDto();
    Object.assign(yearClassToEdit, yearClass);
    this.setupYearClassDialog(yearClassToEdit);
  }


  private setupYearClassDialog(yearClassdto: YearClassAddDto | YearClassUpdateDto): void {
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
          this.doYearClassAdd(typedObject);
        } else if (typedObject instanceof YearClassUpdateDto) {
          this.doYearClassUpdate(typedObject);
        }
      }
    });
  }

  private doYearClassUpdate(dto: YearClassUpdateDto): void {
    this._yearClassClient.updateYearClass(dto)
    .subscribe(
      (savedYearClass) => {
        this.loadYearClasss();
       }
    );
  }

  private doYearClassAdd(dto: YearClassAddDto): void {
    this._yearClassClient.addYearClass(dto)
        .subscribe(
          (savedYearClass) => {
            this.loadYearClasss();
          }
        );
  }

  yearClassSelected(): void {
    this._homeworkClient.getHomeWorkAssignments(this.selectedYearClass.id)
    .subscribe(
      (data) => {
        this.homeWorkAssignments = data;
        this.selectedHomeWorkAssignment = null;
      }
    );
  }

  addHomeWorkAssignment(): void {
    const homeWorkAssignmentToAdd: HomeWorkAssignmentAddDto = new HomeWorkAssignmentAddDto();
    this.setupHomeWorkAssignmentDialog(homeWorkAssignmentToAdd);
  }

  editHomeWorkAssignment(homeWorkAssignment: HomeWorkAssignmentDto): void {
    this.editedHomeWorkAssignment = homeWorkAssignment;
    const homeWorkAssignmentToEdit: HomeWorkAssignmentUpdateDto = new HomeWorkAssignmentUpdateDto();
    Object.assign(homeWorkAssignmentToEdit, homeWorkAssignment);
    this.setupHomeWorkAssignmentDialog(homeWorkAssignmentToEdit);
  }

  copyLinkHomeWorkAssignment(homeWorkAssignment: HomeWorkAssignmentDto): void {
    navigator.clipboard.writeText(window.location.origin + '/homework/' + homeWorkAssignment.id);
  }


  private setupHomeWorkAssignmentDialog(homeWorkAssignmentDto: HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto): void {
    homeWorkAssignmentDto.yearClassId = this.selectedYearClass.id;
    const dialogRef = this.dialog.open(UpsertHomeWorkAssignmentDialogComponent, {
      width: '348px',
      data: homeWorkAssignmentDto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto;
      if (typedObject) {
        if (typedObject instanceof HomeWorkAssignmentAddDto) {
          this.doHomeWorkAssignmentAdd(typedObject);
        } else if (typedObject instanceof HomeWorkAssignmentUpdateDto) {
          this.doHomeWorkAssignmentUpdate(typedObject);
        }
      }
    });
  }

  private doHomeWorkAssignmentUpdate(dto: HomeWorkAssignmentUpdateDto): void {
    this._homeworkClient.updateHomeWorkAssignment(dto)
    .subscribe(
      (savedHomeWorkAssignment) => {
        this.yearClassSelected();
       }
    );
  }

  private doHomeWorkAssignmentAdd(dto: HomeWorkAssignmentAddDto): void {
    this._homeworkClient.addHomeWorkAssignment(dto)
        .subscribe(
          (savedHomeWorkAssignment) => {
            this.yearClassSelected();
          }
        );
  }

  isHomeWorkAssignmentSelected(id: string): string {
    if (this.selectedHomeWorkAssignment && this.selectedHomeWorkAssignment.id === id) {
      return 'home-work-assignmen-selected';
    } else {
      return '';
    }
  }

  viewWords(homeWorkAssignment: HomeWorkAssignmentDto): void {
    this.showWords = true;
    this.showSubmittedWork = false;
    this.getSelectedHomeWorkAssignment(homeWorkAssignment.id);
  }

  viewSubmissions(homeWorkAssignment: HomeWorkAssignmentDto): void {
    this.showWords = false;
    this.showSubmittedWork = true;
    this.getSelectedHomeWorkAssignment(homeWorkAssignment.id);
  }

  private getSelectedHomeWorkAssignment(id: string): void {
    if (this.selectedHomeWorkAssignment && this.selectedHomeWorkAssignment.id === id) {
      return;
    }
    this._homeworkClient.getHomeWorkAssignment(id)
    .subscribe(
      (data) => {
        this.selectedHomeWorkAssignment = data;
      }
    );
  }

  addHomeWorkAssignmentItem(): void {
    const homeWorkAssignmentItemToAdd: HomeWorkAssignmentItemAddDto = new HomeWorkAssignmentItemAddDto();
    homeWorkAssignmentItemToAdd.wordLanguage = this.selectedYearClass.defaultWordLanguage;
    homeWorkAssignmentItemToAdd.sentenceLanguage = this.selectedYearClass.defaultSentenceLanguage;

    this.setupHomeWorkAssignmentItemDialog(homeWorkAssignmentItemToAdd);
  }

  editHomeWorkAssignmentItem(homeWorkAssignmentItem: HomeWorkAssignmentItemDto): void {
    this.editedHomeWorkAssignmentItem = homeWorkAssignmentItem;
    const homeWorkAssignmentItemToEdit: HomeWorkAssignmentItemUpdateDto = new HomeWorkAssignmentItemUpdateDto();
    Object.assign(homeWorkAssignmentItemToEdit, homeWorkAssignmentItem);
    this.setupHomeWorkAssignmentItemDialog(homeWorkAssignmentItemToEdit);
  }

  deleteHomeWorkAssignmentItem(homeWorkAssignmentItem: HomeWorkAssignmentItemDto): void {
    this._homeworkClient.deleteHomeWorkAssignmentItem(homeWorkAssignmentItem.id)
    .subscribe(
      () => {
        const index = this.selectedHomeWorkAssignment.homeWorkAssignmentItems.indexOf(homeWorkAssignmentItem);
        this.selectedHomeWorkAssignment.homeWorkAssignmentItems.splice(index, 1);
      }
    );
  }

  private setupHomeWorkAssignmentItemDialog(
    homeWorkAssignmentItemDto: HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto): void {
    homeWorkAssignmentItemDto.homeWorkAssignmentId = this.selectedHomeWorkAssignment.id;
    const dialogRef = this.dialog.open(UpsertHomeWorkAssignmentItemDialogComponent, {
      width: '348px',
      data: homeWorkAssignmentItemDto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto;
      if (typedObject) {
        if (typedObject instanceof HomeWorkAssignmentItemAddDto) {
          this.doHomeWorkAssignmentItemAdd(typedObject);
        } else if (typedObject instanceof HomeWorkAssignmentItemUpdateDto) {
          this.doHomeWorkAssignmentItemUpdate(typedObject);
        }
      }
    });
  }

  private doHomeWorkAssignmentItemUpdate(dto: HomeWorkAssignmentItemUpdateDto): void {
    this._homeworkClient.updateHomeWorkAssignmentItem(dto)
    .subscribe(
      (savedHomeWorkAssignmentItem) => {
        this.editedHomeWorkAssignmentItem.rowVersion = savedHomeWorkAssignmentItem.rowVersion;
        this.editedHomeWorkAssignmentItem.word = dto.word;
        this.editedHomeWorkAssignmentItem.sentence = dto.sentence;
       }
    );
  }

  private doHomeWorkAssignmentItemAdd(dto: HomeWorkAssignmentItemAddDto): void {
    this._homeworkClient.addHomeWorkAssignmentItem(dto)
        .subscribe(
          (savedHomeWorkAssignmentItem) => {
            const newItem: HomeWorkAssignmentItemDto = new HomeWorkAssignmentItemDto();
            newItem.id = savedHomeWorkAssignmentItem.id;
            newItem.rowVersion = savedHomeWorkAssignmentItem.rowVersion;
            newItem.sentence = dto.sentence;
            newItem.word = dto.word;
            this.selectedHomeWorkAssignment.homeWorkAssignmentItems.push(newItem);
          }
        );
  }
}
