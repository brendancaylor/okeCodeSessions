import * as moment from 'moment';

interface CharacterPair {
    key: string;
    value: string;
}

export class CharacterConstants {

    public static characterPairs: Array<CharacterPair> = [
        {key: 'ç', value: 'c'},
        {key: 'â', value: 'a'},
        {key: 'ê', value: 'e'},
        {key: 'î', value: 'i'},
        {key: 'ô', value: 'o'},
        {key: 'û', value: 'u'},
        {key: 'à', value: 'a'},
        {key: 'è', value: 'e'},
        {key: 'ù', value: 'u'},
        {key: 'ë', value: 'e'},
        {key: 'ï', value: 'i'},
        {key: 'ü', value: 'u'},

        {key: 'ä', value: 'a'},
        {key: 'ö', value: 'o'},
        {key: 'ü', value: 'u'},
        {key: 'ß', value: 's'},

        {key: 'á', value: 'a'},
        {key: 'é', value: 'e'},
        {key: 'í', value: 'i'},
        {key: 'ó', value: 'o'},
        {key: 'ú', value: 'u'},
        {key: 'ñ', value: 'n'},
        {key: 'ü', value: 'u'}
    ];
}

export class HomeWorkAssignmentViewmodel {

    id = '';
    homeworkItems: Array<HomeworkItemViewmodel> = [];
    studentName = '';
    dueDate: moment.Moment = moment();
    yearClassDisplay = '';
    scorePositions: Array<number> = [];
    scoreIncludedPositions: Array<number> = [];

    get isTop(): boolean {
        if (this.scorePositions.length === 0) {
            return true;
        }
        return this.totalScore >= this.scorePositions[this.scorePositions.length - 1];
    }

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
        const attemptReplaceSpecialCharacters = this.replaceSpecialCharacters(this.word.toLowerCase(), this.attempt.toLowerCase());
        return this.word.toLowerCase() === attemptReplaceSpecialCharacters.toLowerCase() && this.word.length === this.attempt.length;
    }

    replaceSpecialCharacters(wordToSpell: string, attempt: string): string {
        let attemptCorrected = '';
        for (let i = 0; i <= this.word.length - 1; i++) {
            const letterWord = this.word.substring(i, i + 1).toLowerCase();
            if (i + 1 <= this.attempt.length) {

                let letterAttempt = this.attempt.substring(i, i + 1).toLowerCase();

                const hasSpecialCharacter = CharacterConstants.characterPairs.find(
                    (characterPair) => {
                        return characterPair.key.toLowerCase() === letterWord.toLowerCase()
                        && characterPair.key.toLowerCase() === letterWord.toLowerCase();
                    }
                );

                if (hasSpecialCharacter) {
                    letterAttempt = hasSpecialCharacter.key;
                }

                attemptCorrected += letterAttempt;
            }
        }
        return attemptCorrected;
    }

    get hint(): string {
        let result = '';
        const numberOfSneakPeaks = 1;
        let numberOfSneakPeaksUsed = 0;
        for (let i = 0; i <= this.word.length - 1; i++) {
            const letterWord = this.word.substring(i, i + 1).toLowerCase();
            if (i + 1 <= this.attempt.length) {
                let letterAttempt = this.attempt.substring(i, i + 1).toLowerCase();

                const hasSpecialCharacter = CharacterConstants.characterPairs.find(
                    (characterPair) => {
                        return characterPair.key.toLowerCase() === letterWord.toLowerCase()
                        && characterPair.key.toLowerCase() === letterWord.toLowerCase();
                    }
                );

                if (hasSpecialCharacter) {
                    letterAttempt = hasSpecialCharacter.key;
                }

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
