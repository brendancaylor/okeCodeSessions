import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeWorkAssignmentItemAddDto, HomeWorkAssignmentItemUpdateDto, StandardListItemDto } from 'src/app/core/services/clients';

export class UpsertGenericWordViewmodel {

    upsertGenericWordForm = this.fb.group({
        word: ['', [Validators.required]],
        sentence: ['', Validators.required],
        wordLanguage: ['', Validators.required],
        sentenceLanguage: ['', Validators.required]
      });


    constructor(private upsertGenericWordDto: StandardListItemDto | HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto,
        private fb: FormBuilder) {

        if (upsertGenericWordDto.word) {
            this.upsertGenericWordForm.patchValue({
                word: upsertGenericWordDto.word,
            });
        }

        if (upsertGenericWordDto.sentence) {
            this.upsertGenericWordForm.patchValue({
                sentence: upsertGenericWordDto.sentence,
            });
        }

        if (upsertGenericWordDto.wordLanguage) {
            this.upsertGenericWordForm.patchValue({
                wordLanguage: upsertGenericWordDto.wordLanguage
            });
        }

        if (upsertGenericWordDto.sentenceLanguage) {
            this.upsertGenericWordForm.patchValue({
                sentenceLanguage: upsertGenericWordDto.sentenceLanguage
            });
        }

    }

    public getDto(): StandardListItemDto | HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto {
        this.upsertGenericWordDto.word = this.upsertGenericWordForm.value.word;
        this.upsertGenericWordDto.sentence = this.upsertGenericWordForm.value.sentence;
        this.upsertGenericWordDto.wordLanguage = this.upsertGenericWordForm.value.wordLanguage;
        this.upsertGenericWordDto.sentenceLanguage = this.upsertGenericWordForm.value.sentenceLanguage;
        return this.upsertGenericWordDto;
    }
}
