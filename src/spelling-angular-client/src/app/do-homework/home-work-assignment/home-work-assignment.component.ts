import { Component, OnInit } from '@angular/core';
import { HomeWorkAssignmentDto } from 'src/app/core/services/clients';
import { HomeWorkAssignmentViewmodel, HomeworkItemViewmodel } from './home-work-assignment-viewmodel';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home-work-assignment',
  templateUrl: './home-work-assignment.component.html',
  styleUrls: ['./home-work-assignment.component.scss']
})
export class HomeWorkAssignmentComponent implements OnInit {

  viewmodel: HomeWorkAssignmentViewmodel = new HomeWorkAssignmentViewmodel();
  canSubmit = false;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.forEach(
      (data) => {
    if (data.homeworkData) {
      const dto = data.homeworkData as HomeWorkAssignmentDto;
      this.viewmodel.id = dto.id;
      this.viewmodel.homeworkItems = dto.homeWorkAssignmentItems.map(
        (homeWorkAssignmentItem) => {
          const homeworkItem: HomeworkItemViewmodel = new HomeworkItemViewmodel();
          homeworkItem.id = homeWorkAssignmentItem.id;
          homeworkItem.sentence = homeWorkAssignmentItem.sentence;
          homeworkItem.word = homeWorkAssignmentItem.word;
          return homeworkItem;
        }
      );
    }
  });

  }

  try(homeworkItem: HomeworkItemViewmodel): void {
    if (!homeworkItem.isCorrect) {
      homeworkItem.snapshotHint = homeworkItem.hint;
      homeworkItem.score --;
    }

    if (this.viewmodel.allCorrect) {
      this.canSubmit = true;
    }
  }
}
