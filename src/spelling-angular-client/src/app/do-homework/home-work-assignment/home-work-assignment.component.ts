import { Component, OnInit } from '@angular/core';
import {
  HomeWorkAssignmentDto,
  SpeachClient,
  SpeechType,
  HomeworkClient,
  SubmittedHomeWorkAddDto,
  HomeWorkAssignmentItemDto
} from 'src/app/core/services/clients';
import { HomeWorkAssignmentViewmodel, HomeworkItemViewmodel } from './home-work-assignment-viewmodel';
import { Router, ActivatedRoute } from '@angular/router';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { HomeWorkHelpComponent } from './home-work-help.component';

@Component({
  selector: 'app-home-work-assignment',
  templateUrl: './home-work-assignment.component.html',
  styleUrls: ['./home-work-assignment.component.scss']
})
export class HomeWorkAssignmentComponent implements OnInit {

  viewmodel: HomeWorkAssignmentViewmodel | null = null;
  isSubmitted = false;
  SpeechTypeEnum = SpeechType;
  soundIsBeingPlayed = false;
  private originalData: HomeWorkAssignmentDto | null = null;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private _speachClient: SpeachClient,
    private _homeworkClient: HomeworkClient,
    private _bottomSheet: MatBottomSheet
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
          dto!.submittedHomeWorks!.forEach(
            (submittedHomeWork) => {

              if (!this.viewmodel!
                .scorePositions
                .some(
                  (scorePosition) => {
                  return scorePosition === submittedHomeWork.score;
                })
              ) {
                this.viewmodel!.scorePositions.push(submittedHomeWork.score);
              }
            }
          );
          this.sortPositions();
          this.loadViewmodelHomeworkItems();
          this.wedgeScore();
        } else {
          this.router.navigate(['/']);
        }
    });
  }

  sortPositions(): void {
    this.viewmodel!.scorePositions.sort((a, b) => a < b ? -1 : 1);
  }

  loadViewmodelHomeworkItems(): void {
    this.viewmodel!.homeworkItems = this.originalData!.homeWorkAssignmentItems!.map(
      (originalDataHomeWorkAssignmentItem) => {
        const homeworkItem: HomeworkItemViewmodel = new HomeworkItemViewmodel();
        homeworkItem.id = originalDataHomeWorkAssignmentItem.id;
        homeworkItem.sentence = originalDataHomeWorkAssignmentItem.sentence;
        homeworkItem.word = originalDataHomeWorkAssignmentItem.word;
        this.loadSound(homeworkItem);
        return homeworkItem;
      }
    );
  }

  restartHomework(): void {
    this.viewmodel!.homeworkItems.forEach(
      (homeworkItem) => {
        homeworkItem.snapshotHint = '';
        homeworkItem.attempt = '';
        homeworkItem.score = 10;
        homeworkItem.correctTry = undefined;
      }
    );
    this.wedgeScore();
  }

  private loadSound(homeworkItem: HomeworkItemViewmodel) {
    console.log(homeworkItem.id);
    this._speachClient.getSpeach(homeworkItem.id, SpeechType.Word)
    .subscribe(
      (blob) => {
        console.log('sound:' + homeworkItem.id);
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

    // tslint:disable-next-line: max-line-length
    // const url = 'https://bili-spell-api.azurewebsites.net/api/Speach?homeWorkAssignmentItemId=dee401cc-692f-444f-b725-319edf0ee2a5&speechType=0';
    // const audioDiv = document.getElementById('audioDiv');
    // const newAudio = new Audio();
    // const srcElement = document.createElement('source');
    // srcElement.setAttribute('src', url);
    // srcElement.setAttribute('type', 'audio/mpeg');
    // newAudio.append(srcElement);
    // newAudio.autoplay = true;

    // audioDiv.appendChild(newAudio);
    // return;

    this.soundIsBeingPlayed = true;
    let blobUrl = '';
    if (speechType === SpeechType.Word) {
      blobUrl = URL.createObjectURL(homeworkItem.wordAsMp3);
      // blobContent = homeworkItem.wordAsMp3;
    } else if (speechType === SpeechType.Sentence) {
      blobUrl = URL.createObjectURL(homeworkItem.sentenceAsMp3);
      // blobContent = homeworkItem.wordAsMp3;
    }

    const audio: HTMLAudioElement = document.getElementById('audioVisable') as HTMLAudioElement;
    const audioSource: HTMLSourceElement = document.getElementById('audioSource') as HTMLSourceElement;
    audioSource.src = blobUrl;
    audio.load();
    audio.play();
    const self = this;
    audio.onended = function() {
        self.soundIsBeingPlayed = false;
        if (homeworkItem !== null) {
          document.getElementById(homeworkItem.id)!.focus();
        }
        
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

  getNextUnCompletedWord(): HomeworkItemViewmodel | null {
    const foundItems = this.viewmodel!.homeworkItems.filter(
      (homeworkItem) => !homeworkItem.correctTry
    );
    if (foundItems.length > 0) {
      return foundItems[0];
    } else {
      return null;
    }
  }

  try(homeworkItem: HomeworkItemViewmodel): void {
    let indexOfItem = this.viewmodel!.homeworkItems.indexOf(homeworkItem);
    indexOfItem ++;
    homeworkItem.snapshotHint = homeworkItem.hint;
    if (homeworkItem.isCorrect) {
      homeworkItem.correctTry = true;
      this.playCorrect();

      const lastItem = this.getNextUnCompletedWord();
      if (lastItem) {
        document.getElementById(lastItem.id)!.focus();
        setTimeout(
          () => {
            this.playSound(lastItem, SpeechType.Word);
          }, 1500
        );
      } else if (this.viewmodel!.studentName.length < 1) {
        document.getElementById('studentName')!.focus();
      }

    } else {
      homeworkItem.correctTry = false;
      homeworkItem.score --;
      this.playIncorrect();
      setTimeout(() => {
        document.getElementById(homeworkItem.id)!.focus();
      }, 500);
    }
    this.wedgeScore();
  }

  wedgeScore(): void {

    this.viewmodel!.scoreIncludedPositions = this.viewmodel!.scorePositions.map((score) => score);
    let isPlaced = false;
    const indexOfTotalScore = this.viewmodel!.scoreIncludedPositions.indexOf(this.viewmodel!.totalScore);
    if (indexOfTotalScore !== -1) {
      // this.viewmodel.scoreIncludedPositions.splice(indexOfTotalScore, 0, this.viewmodel.totalScore);
      return;
    }

    if (this.viewmodel!.scoreIncludedPositions.length === 0) {
      this.viewmodel!.scoreIncludedPositions.push(this.viewmodel!.totalScore);
        return;
    }

    if (this.viewmodel!.scoreIncludedPositions.length > 0
      && this.viewmodel!.totalScore < this.viewmodel!.scoreIncludedPositions[0]) {
        this.viewmodel!.scoreIncludedPositions.unshift(this.viewmodel!.totalScore);
        return;
    }

    if (this.viewmodel!.scoreIncludedPositions.length > 0
      && this.viewmodel!.totalScore > this.viewmodel!.scoreIncludedPositions[length - 1]) {
        this.viewmodel!.scoreIncludedPositions.push(this.viewmodel!.totalScore);
        return;
    }

    this.viewmodel!.scoreIncludedPositions.forEach(
      (score) => {
        if (this.viewmodel!.totalScore < score && !isPlaced) {
          const indexOfScore = this.viewmodel!.scoreIncludedPositions.indexOf(score);
          this.viewmodel!.scoreIncludedPositions.splice(indexOfScore, 0, this.viewmodel!.totalScore);
          isPlaced = true;
          return;
        }
      }
    );
  }
  //

  submittEnabled(): boolean {
    return this.viewmodel!.allCorrectTry && this.viewmodel!.studentName !== null && this.viewmodel!.studentName.length > 1;
  }

  submit() {
    const dto: SubmittedHomeWorkAddDto = new SubmittedHomeWorkAddDto();
    dto.homeWorkAssignmentId = this.viewmodel!.id;
    dto.score = this.viewmodel!.totalScore;
    dto.studentName = this.viewmodel!.studentName;
    this._homeworkClient.addSubmittedHomeWork(dto)
    .subscribe(
      () => {
        this.isSubmitted = true;
      }
    );
  }

  openHelp(): void {
    this._bottomSheet.open(HomeWorkHelpComponent);
  }
}
