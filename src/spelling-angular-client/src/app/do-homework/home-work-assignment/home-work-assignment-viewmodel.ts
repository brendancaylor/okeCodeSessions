import * as moment from 'moment';

export class HomeWorkAssignmentViewmodel {

    id = '';
    homeworkItems: Array<HomeworkItemViewmodel> = [];
    studentName = '';
    dueDate: moment.Moment = moment();
    yearClassDisplay = '';

    get totalScore(): number {
        return this.homeworkItems.reduce((a, b) => a + b.score, 0);
    }

    get allCorrect(): boolean {
        let result = true;

        this.homeworkItems.forEach(
            (homeworkItem) => {
                if (!homeworkItem.isCorrect) {
                    result = false;
                }
            }
        );
        return result;
    }

    get allCorrectTry(): boolean {
        let result = true;

        this.homeworkItems.forEach(
            (homeworkItem) => {
                if (!homeworkItem.correctTry) {
                    result = false;
                }
            }
        );
        return result;
    }
}

export class HomeworkItemViewmodel {
    id = '';
    isSelected = false;
    word = '';
    attempt = '';
    sentence = '';
    wordAsMp3: Blob = null;
    sentenceAsMp3: Blob = null;
    snapshotHint = '';
    score = 10;
    correctTry?: boolean | undefined;

    get isCorrect(): boolean {
        return this.word.toLowerCase() === this.attempt.toLowerCase();
    }

    get hint(): string {
        let result = '';
        const numberOfSneakPeaks = 1;
        let numberOfSneakPeaksUsed = 0;
        for (let i = 0; i <= this.word.length - 1; i++) {
            const letterWord = this.word.substring(i, i + 1).toLowerCase();
            if (i + 1 <= this.attempt.length) {
                const letterAttempt = this.attempt.substring(i, i + 1).toLowerCase();
                if (letterWord === letterAttempt) {
                    result += letterWord;
                } else if (numberOfSneakPeaksUsed < numberOfSneakPeaks) {
                    numberOfSneakPeaksUsed ++;
                    result += letterWord;
                } else {
                    result += '?';
                }
            } else {

                if (numberOfSneakPeaksUsed < numberOfSneakPeaks) {
                    numberOfSneakPeaksUsed ++;
                    result += letterWord;
                } else {
                    result += '?';
                }

            }
        }
        return result;
    }
}
