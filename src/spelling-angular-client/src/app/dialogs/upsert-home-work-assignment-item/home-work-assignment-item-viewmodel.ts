import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeWorkAssignmentItemAddDto, HomeWorkAssignmentItemUpdateDto } from 'src/app/core/services/clients';

export class HomeWorkAssignmentItemViewmodel {

    homeWorkAssignmentItemForm = this.fb.group({
        word: ['', [Validators.required]],
        sentence: ['', Validators.required],
        wordLanguage: ['', Validators.required],
        sentenceLanguage: ['', Validators.required]
      });


    constructor(private homeWorkAssignmentItemDto: HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto,
        private fb: FormBuilder) {

        if (homeWorkAssignmentItemDto.word) {
            this.homeWorkAssignmentItemForm.patchValue({
                word: homeWorkAssignmentItemDto.word,
            });
        }

        if (homeWorkAssignmentItemDto.sentence) {
            this.homeWorkAssignmentItemForm.patchValue({
                sentence: homeWorkAssignmentItemDto.sentence,
            });
        }

        if (homeWorkAssignmentItemDto.wordLanguage) {
            this.homeWorkAssignmentItemForm.patchValue({
                wordLanguage: homeWorkAssignmentItemDto.wordLanguage
            });
        }

        if (homeWorkAssignmentItemDto.sentenceLanguage) {
            this.homeWorkAssignmentItemForm.patchValue({
                sentenceLanguage: homeWorkAssignmentItemDto.sentenceLanguage
            });
        }

    }

    public getDto(): HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto {
        this.homeWorkAssignmentItemDto.word = this.homeWorkAssignmentItemForm.value.word;
        this.homeWorkAssignmentItemDto.sentence = this.homeWorkAssignmentItemForm.value.sentence;
        this.homeWorkAssignmentItemDto.wordLanguage = this.homeWorkAssignmentItemForm.value.wordLanguage;
        this.homeWorkAssignmentItemDto.sentenceLanguage = this.homeWorkAssignmentItemForm.value.sentenceLanguage;
        return this.homeWorkAssignmentItemDto;
    }
}
