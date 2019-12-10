import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeWorkAssignmentItemAddDto, HomeWorkAssignmentItemUpdateDto } from 'src/app/core/services/clients';

export class HomeWorkAssignmentItemViewmodel {

    homeWorkAssignmentItemForm = this.fb.group({
        dueDate: ['', [Validators.required]]
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

    }

    public getDto(): HomeWorkAssignmentItemAddDto | HomeWorkAssignmentItemUpdateDto {
        this.homeWorkAssignmentItemDto.word = this.homeWorkAssignmentItemForm.value.word;
        this.homeWorkAssignmentItemDto.word = this.homeWorkAssignmentItemForm.value.word;
        return this.homeWorkAssignmentItemDto;
    }
}
