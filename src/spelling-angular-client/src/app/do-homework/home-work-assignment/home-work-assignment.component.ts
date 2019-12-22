import { Component, OnInit } from '@angular/core';
import { HomeWorkAssignmentDto, SpeachClient, SpeechType, HomeworkClient, SubmittedHomeWorkAddDto } from 'src/app/core/services/clients';
import { HomeWorkAssignmentViewmodel, HomeworkItemViewmodel } from './home-work-assignment-viewmodel';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home-work-assignment',
  templateUrl: './home-work-assignment.component.html',
  styleUrls: ['./home-work-assignment.component.scss']
})
export class HomeWorkAssignmentComponent implements OnInit {

  viewmodel: HomeWorkAssignmentViewmodel = null;
  isSubmitted = false;
  SpeechTypeEnum = SpeechType;
  soundIsBeingPlayed = false;
  private originalData: HomeWorkAssignmentDto;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private _speachClient: SpeachClient,
    private _homeworkClient: HomeworkClient
    ) { }

  ngOnInit() {
    if (!this.activeRoute.data) {
      return;
    }

    this.activeRoute.data.forEach(
      (data) => {
        if (data.homeworkData) {
          const dto = data.homeworkData as HomeWorkAssignmentDto;
          this.originalData = dto;
          this.viewmodel = new HomeWorkAssignmentViewmodel();
          this.viewmodel.id = dto.id;
          this.viewmodel.dueDate = dto.dueDate;
          this.viewmodel.yearClassDisplay = dto.yearClassDisplay;
          this.reset();
        } else {
          this.router.navigate(['/']);
        }
    });
  }

  reset(): void {
    this.viewmodel.homeworkItems = this.originalData.homeWorkAssignmentItems.map(
      (homeWorkAssignmentItem) => {
        const homeworkItem: HomeworkItemViewmodel = new HomeworkItemViewmodel();
        homeworkItem.id = homeWorkAssignmentItem.id;
        homeworkItem.sentence = homeWorkAssignmentItem.sentence;
        homeworkItem.word = homeWorkAssignmentItem.word;
        this.loadSound(homeworkItem);
        return homeworkItem;
      }
    );
  }

  private loadSound(homeworkItem: HomeworkItemViewmodel) {
    this._speachClient.getSpeach(homeworkItem.id, SpeechType.Word)
    .subscribe(
      (blob) => {
        homeworkItem.wordAsMp3 = blob;
      }
    );

    this._speachClient.getSpeach(homeworkItem.id, SpeechType.Sentence)
    .subscribe(
      (blob) => {
        homeworkItem.sentenceAsMp3 = blob;
      }
    );

  }

  playSound(homeworkItem: HomeworkItemViewmodel, speechType: SpeechType): void {
    this.soundIsBeingPlayed = true;
    let blobUrl = '';
    if (speechType === SpeechType.Word) {
      blobUrl = URL.createObjectURL(homeworkItem.wordAsMp3);
    } else if (speechType === SpeechType.Sentence) {
      blobUrl = URL.createObjectURL(homeworkItem.sentenceAsMp3);
    }
    const audio: HTMLAudioElement = document.getElementById('audioHidden') as HTMLAudioElement;
    audio.src = blobUrl;
    audio.load();
    audio.play();
    const self = this;
    audio.onended = function() {
        self.soundIsBeingPlayed = false;
        document.getElementById(homeworkItem.id).focus();
      };
  }

  playCorrect(): void {
    const audio = new Audio();
    audio.src = '/assets/correct.mp3';
    audio.load();
    audio.play();
  }

  playIncorrect(): void {
    const audio = new Audio();
    audio.src = '/assets/wrong.mp3';
    audio.load();
    audio.play();
  }

  getNextUnCompletedWord(): HomeworkItemViewmodel {
    const foundItems = this.viewmodel.homeworkItems.filter(
      (homeworkItem) => !homeworkItem.correctTry
    );
    if (foundItems.length > 0) {
      return foundItems[0];
    } else {
      return null;
    }
  }

  try(homeworkItem: HomeworkItemViewmodel): void {
    let indexOfItem = this.viewmodel.homeworkItems.indexOf(homeworkItem);
    indexOfItem ++;
    homeworkItem.snapshotHint = homeworkItem.hint;
    if (homeworkItem.isCorrect) {
      homeworkItem.correctTry = true;
      this.playCorrect();

      const lastItem = this.getNextUnCompletedWord();
      if (lastItem) {
        document.getElementById(lastItem.id).focus();
        setTimeout(
          () => {
            this.playSound(lastItem, SpeechType.Word);
          }, 2000
        );
      } else if (this.viewmodel.studentName.length < 1) {
        document.getElementById('studentName').focus();
      }

    } else {
      homeworkItem.correctTry = false;
      homeworkItem.score --;
      this.playIncorrect();
      setTimeout(() => {
        document.getElementById(homeworkItem.id).focus();
      }, 1000);
    }
  }

  submittEnabled(): boolean {
    return this.viewmodel.allCorrectTry && this.viewmodel.studentName && this.viewmodel.studentName.length > 1;
  }

  submit() {
    const dto: SubmittedHomeWorkAddDto = new SubmittedHomeWorkAddDto();
    dto.homeWorkAssignmentId = this.viewmodel.id;
    dto.score = this.viewmodel.totalScore;
    dto.studentName = this.viewmodel.studentName;
    this._homeworkClient.addSubmittedHomeWork(dto)
    .subscribe(
      () => {
        this.isSubmitted = true;
      }
    );
  }
}
