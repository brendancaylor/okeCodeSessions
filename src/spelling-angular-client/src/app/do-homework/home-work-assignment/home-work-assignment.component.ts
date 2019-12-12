import { Component, OnInit } from '@angular/core';
import { HomeWorkAssignmentDto } from 'src/app/core/services/clients';
import { HomeWorkAssignmentViewmodel, HomeworkItemViewmodel } from './home-work-assignment-viewmodel';

@Component({
  selector: 'app-home-work-assignment',
  templateUrl: './home-work-assignment.component.html',
  styleUrls: ['./home-work-assignment.component.scss']
})
export class HomeWorkAssignmentComponent implements OnInit {

  viewmodel: HomeWorkAssignmentViewmodel = new HomeWorkAssignmentViewmodel();
  canSubmit = false;
  constructor() { }

  ngOnInit() {
    const homeworkItem1: HomeworkItemViewmodel = new HomeworkItemViewmodel();
    homeworkItem1.word = 'something';

    const homeworkItem2: HomeworkItemViewmodel = new HomeworkItemViewmodel();
    homeworkItem2.word = 'another';

    this.viewmodel.homeworkItems.push(homeworkItem1);
    this.viewmodel.homeworkItems.push(homeworkItem2);
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
