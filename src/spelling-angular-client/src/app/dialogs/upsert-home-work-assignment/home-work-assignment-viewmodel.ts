import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeWorkAssignmentAddDto, HomeWorkAssignmentUpdateDto } from 'src/app/core/services/clients';

export class HomeWorkAssignmentViewmodel {

    homeWorkAssignmentForm = this.fb.group({
        dueDate: ['', [Validators.required]]
      });


    constructor(private homeWorkAssignmentDto: HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto, private fb: FormBuilder) {

        if (homeWorkAssignmentDto.dueDate) {
            this.homeWorkAssignmentForm.patchValue({
                dueDate: homeWorkAssignmentDto.dueDate,
            });
        }
    }

    public getDto(): HomeWorkAssignmentAddDto | HomeWorkAssignmentUpdateDto {
        this.homeWorkAssignmentDto.dueDate = this.homeWorkAssignmentForm.value.dueDate;
        return this.homeWorkAssignmentDto;
    }
}
