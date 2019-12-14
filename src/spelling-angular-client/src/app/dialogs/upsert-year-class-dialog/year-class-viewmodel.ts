import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { YearClassAddDto, YearClassUpdateDto } from 'src/app/core/services/clients';

export class YearClassViewmodel {

    yearClassForm = this.fb.group({
        teacherName: ['', Validators.required],
        yearClassName: ['', Validators.required],
        defaultWordLanguage: ['', Validators.required],
        defaultSentenceLanguage: ['', Validators.required]
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

        if (yearClassDto.defaultWordLanguage) {
            this.yearClassForm.patchValue({
                defaultWordLanguage: yearClassDto.defaultWordLanguage
            });
        }

        if (yearClassDto.defaultSentenceLanguage) {
            this.yearClassForm.patchValue({
                defaultSentenceLanguage: yearClassDto.defaultSentenceLanguage
            });
        }
    }

    public getDto(): YearClassAddDto | YearClassUpdateDto {
        this.yearClassDto.teacherName = this.yearClassForm.value.teacherName;
        this.yearClassDto.yearClassName = this.yearClassForm.value.yearClassName;
        this.yearClassDto.defaultWordLanguage = this.yearClassForm.value.defaultWordLanguage;
        this.yearClassDto.defaultSentenceLanguage = this.yearClassForm.value.defaultSentenceLanguage;
        return this.yearClassDto;
    }
}
