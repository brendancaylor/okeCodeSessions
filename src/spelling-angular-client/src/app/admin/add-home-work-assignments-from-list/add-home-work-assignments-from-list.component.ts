import { Component, OnInit } from '@angular/core';
import {
  HomeworkClient,
  LookupDto,
  StandardListDto,
  StandardListItemDto,
  SpeechType,
  AddHomeworkFromList,
  AddHomeworkAssignmentFromList,
  YearClassClient,
  YearClassDto} from 'src/app/core/services/clients';
import { HomeWorkAssignmentsViewmodel, HomeWorkAssignmentSplit } from './home-work-assignments-viewmodel';
import { FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { MatDatepickerInputEvent } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-home-work-assignments-from-list',
  templateUrl: './add-home-work-assignments-from-list.component.html',
  styleUrls: ['./add-home-work-assignments-from-list.component.scss']
})
export class AddHomeWorkAssignmentsFromListComponent implements OnInit {

  standardLists: Array<LookupDto> = [];
  selectedStandardListLookup: LookupDto;
  selectedStandardList: StandardListDto;
  SpeechTypeEnum = SpeechType;
  soundIsBeingPlayed = false;
  homeWorkAssignmentsViewmodel: HomeWorkAssignmentsViewmodel;
  yearClass: YearClassDto;
  constructor(
    private readonly route: ActivatedRoute,
    private router: Router,
    private _homeworkClient: HomeworkClient,
    private _yearClassClient: YearClassClient,
    private fb: FormBuilder) {
      this.homeWorkAssignmentsViewmodel = new HomeWorkAssignmentsViewmodel(fb);
    }

  ngOnInit() {
    const yearClassId = this.route.snapshot.params['yearClassId'];
    this._homeworkClient.getStandardLists()
    .subscribe(
      (data) => {
        this.standardLists = data;
      }
    );

    this._yearClassClient.getYearClass(yearClassId)
    .subscribe(
      (data) => {
        this.yearClass = data;
      });
  }

  standardListSelected(): void {
    this._homeworkClient.getStandardList(this.selectedStandardListLookup.id)
    .subscribe(
      (data) => {
        this.selectedStandardList = data;
        this.splitAssignments();
      }
    );
  }

  addDate(type: string, event: MatDatepickerInputEvent<moment.Moment>) {
    const test = event.value;
  }

  splitAssignments(): void {

    this.homeWorkAssignmentsViewmodel.homeWorkAssignmentSplits = [];
    const numberAssignments = Math.ceil(this.selectedStandardList.standardListItems.length /
    this.homeWorkAssignmentsViewmodel.homeWorkAssignmentForm.value.wordsPerAssignment);

    const dueDate = (this.homeWorkAssignmentsViewmodel.homeWorkAssignmentForm.value.dueDate as moment.Moment).clone();
    dueDate.subtract(7, 'days');
    for (let index = 0; index < numberAssignments; index++) {

      const daysToAdd = (index + 1) * 7;
      const runningDate = dueDate.clone().add(daysToAdd, 'days');
      const homeWorkAssignmentSplit: HomeWorkAssignmentSplit = new HomeWorkAssignmentSplit();
      homeWorkAssignmentSplit.dueDate = runningDate;

      const startPage = index * this.homeWorkAssignmentsViewmodel.homeWorkAssignmentForm.value.wordsPerAssignment;
      let endPage = startPage + this.homeWorkAssignmentsViewmodel.homeWorkAssignmentForm.value.wordsPerAssignment;
      if (endPage >= this.selectedStandardList.standardListItems.length) {
        endPage = this.selectedStandardList.standardListItems.length;
      }

      homeWorkAssignmentSplit.standardListItems = this.selectedStandardList.standardListItems.slice(startPage, endPage);
      this.homeWorkAssignmentsViewmodel.homeWorkAssignmentSplits.push(homeWorkAssignmentSplit);
    }

  }

  save() {
    const dto: AddHomeworkFromList = new AddHomeworkFromList();
    dto.standardListId = this.selectedStandardList.id;
    dto.yearClassId = this.yearClass.id;
    dto.addHomeworkAssignments = [];
    this.homeWorkAssignmentsViewmodel.homeWorkAssignmentSplits.forEach(
      (homeWorkAssignmentSplit) => {

        const homeworkAssignment: AddHomeworkAssignmentFromList = new AddHomeworkAssignmentFromList();
        homeworkAssignment.dueDate = homeWorkAssignmentSplit.dueDate;
        homeworkAssignment.standardListItemIds = [];
        homeWorkAssignmentSplit.standardListItems.forEach(
          (standardListItem) => {
            homeworkAssignment.standardListItemIds.push(standardListItem.id);
          }
        );

        dto.addHomeworkAssignments.push(homeworkAssignment);
      }
    );

    this._homeworkClient.addHomeWorkAssignmentsFromList(dto)
    .subscribe(
      () => {
        this.router.navigate([`/admin/manage-homework/`]);
      }
    );
  }
}
