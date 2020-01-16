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
import { MatTableDataSource, MatDialog, MatSnackBar, MatMenu } from '@angular/material';
import {
  UpsertHomeWorkAssignmentDialogComponent
} from 'src/app/dialogs/upsert-home-work-assignment/upsert-home-work-assignment-dialog.component';
import {
  UpsertGenericWordDialogComponent
} from 'src/app/dialogs/upsert-generic-word/upsert-generic-word-dialog.component';
import { WaitingDialogComponent } from 'src/app/dialogs/waiting-dialog/waiting-dialog.component';
import { Router, ActivatedRoute } from '@angular/router';
import { AreYouSureDialogComponent } from 'src/app/dialogs/are-you-sure-dialog/are-you-sure-dialog.component';

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

  showWords = false;
  showSubmittedWork = false;
  expansionPanelExpanded = true;

  _currentHomeWorkAssignment: HomeWorkAssignmentDto;
  get currentHomeWorkAssignment(): HomeWorkAssignmentDto {
    this._currentHomeWorkAssignment = null;
    if (this.homeWorkAssignments && this.homeWorkAssignments.length > 0) {
      const furtureAssignments = this.homeWorkAssignments
      .filter(
        (homeWorkAssignment) => homeWorkAssignment.dueDate > moment()
      );
      if (furtureAssignments && furtureAssignments.length > 0) {
        this._currentHomeWorkAssignment = furtureAssignments[0];
      }
    }
    return this._currentHomeWorkAssignment;
  }

  _remainingHomeWorkAssignments: Array<HomeWorkAssignmentDto>;
  get remainingHomeWorkAssignments(): Array<HomeWorkAssignmentDto> {
    if (this.currentHomeWorkAssignment) {
      return this.homeWorkAssignments.filter(
        (homeWorkAssignment) => this.currentHomeWorkAssignment.id !== homeWorkAssignment.id
      );
    } else {
      return this.homeWorkAssignments;
    }
  }

  constructor(
    private _yearClassClient: YearClassClient,
    private _homeworkClient: HomeworkClient,
    private _snackBar: MatSnackBar,
    private router: Router,
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
      data: yearClassdto,
      disableClose: true
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
        this.expansionPanelExpanded = false;
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

  deleteHomeWorkAssignment(homeWorkAssignment: HomeWorkAssignmentDto): void {
    const dialogRef = this.dialog.open(AreYouSureDialogComponent, {
      width: '348px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      if (dialogObject) {
        this._homeworkClient.deleteHomeWorkAssignment(homeWorkAssignment.id).subscribe(
          () => {
            const index = this.homeWorkAssignments.indexOf(homeWorkAssignment);
            this.homeWorkAssignments.splice(index, 1);
          }
        );
      }
    }
    );
  }

  getLink(homeWorkAssignment: HomeWorkAssignmentDto) {
    return window.location.origin + '/homework/' + homeWorkAssignment.id;
  }

  copyText(val: string) {

    if (navigator.clipboard) {
      navigator.clipboard.writeText(val);
    } else {
      const selBox = document.createElement('textarea');
      selBox.style.position = 'fixed';
      selBox.style.left = '0';
      selBox.style.top = '0';
      selBox.style.opacity = '0';
      selBox.value = val;
      document.body.appendChild(selBox);
      selBox.focus();
      selBox.select();
      document.execCommand('copy');
      document.body.removeChild(selBox);
    }

    this._snackBar.open('Homework link copied to clipboard.', 'close', {
      duration: 2000,
    });
  }

  private setupHomeWorkAssignmentDialog(homeWorkAssignmentDto: HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto): void {
    homeWorkAssignmentDto.yearClassId = this.selectedYearClass.id;
    const dialogRef = this.dialog.open(UpsertHomeWorkAssignmentDialogComponent, {
      width: '348px',
      data: homeWorkAssignmentDto,
      disableClose: true
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

  selectHomeWorkAssignment(homeWorkAssignment: HomeWorkAssignmentDto): void {
    this.getSelectedHomeWorkAssignment(homeWorkAssignment.id);
    this.viewWords();
  }

  viewWords(): void {
    this.showWords = true;
    this.showSubmittedWork = false;
    // if (event) {
    //   event.stopPropagation();
    // }
  }

  viewSubmissions(): void {
    this.showWords = false;
    this.showSubmittedWork = true;
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

  addHomeWorkAssignmentsFromList(): void {
    this.router.navigate([`/admin/homeWork-assignments-from-list/${this.selectedYearClass.id}`]);
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
    const dialogRef = this.dialog.open(UpsertGenericWordDialogComponent, {
      width: '348px',
      data: homeWorkAssignmentItemDto,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto;
      typedObject.word = typedObject.word.trim();
      typedObject.sentence = typedObject.sentence.trim();
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

    const dialogRef = this.dialog.open(WaitingDialogComponent, {
      width: '348px',
      disableClose: true
    });

    this._homeworkClient.updateHomeWorkAssignmentItem(dto)
    .subscribe(
      (savedHomeWorkAssignmentItem) => {
        this.editedHomeWorkAssignmentItem.rowVersion = savedHomeWorkAssignmentItem.rowVersion;
        this.editedHomeWorkAssignmentItem.word = dto.word;
        this.editedHomeWorkAssignmentItem.sentence = dto.sentence;
        dialogRef.close();
       },
       (error) => {
         dialogRef.close();
       }
    );
  }

  private doHomeWorkAssignmentItemAdd(dto: HomeWorkAssignmentItemAddDto): void {

    const dialogRef = this.dialog.open(WaitingDialogComponent, {
      width: '348px',
      disableClose: true
    });

    this._homeworkClient.addHomeWorkAssignmentItem(dto)
        .subscribe(
          (savedHomeWorkAssignmentItem) => {
            const newItem: HomeWorkAssignmentItemDto = new HomeWorkAssignmentItemDto();
            newItem.id = savedHomeWorkAssignmentItem.id;
            newItem.rowVersion = savedHomeWorkAssignmentItem.rowVersion;
            newItem.sentence = dto.sentence;
            newItem.word = dto.word;
            this.selectedHomeWorkAssignment.homeWorkAssignmentItems.push(newItem);
            dialogRef.close();
          }
        );
  }
}
