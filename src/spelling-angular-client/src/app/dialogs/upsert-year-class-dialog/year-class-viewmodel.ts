import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { YearClassAddDto, YearClassUpdateDto } from 'src/app/core/services/clients';

export class YearClassViewmodel {

    yearClassForm = this.fb.group({
        teacherName: ['', Validators.required],
        yearClassName: ['', Validators.required]
      });


    constructor(private yearClassDto: YearClassAddDto | YearClassUpdateDto, private fb: FormBuilder) {

        if (yearClassDto.teacherName) {
            this.yearClassForm.patchValue({
                teacherName: yearClassDto.teacherName,
            });
        }

        if (yearClassDto.yearClassName) {
            this.yearClassForm.patchValue({
                yearClassName: yearClassDto.yearClassName,
            });
        }
    }

    public getDto(): YearClassAddDto | YearClassUpdateDto {
        this.yearClassDto.teacherName = this.yearClassForm.value.teacherName;
        this.yearClassDto.yearClassName = this.yearClassForm.value.yearClassName;
        return this.yearClassDto;
    }
}
