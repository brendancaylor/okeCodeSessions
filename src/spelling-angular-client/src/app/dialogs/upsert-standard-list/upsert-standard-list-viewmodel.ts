import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StandardListDto } from 'src/app/core/services/clients';

export class UpsertStandardListViewmodel {

    standardListForm = this.fb.group({
        standardListName: ['', [Validators.required]],
        defaultWordLanguage: ['', Validators.required],
        defaultSentenceLanguage: ['', Validators.required]
      });

    constructor(private standardListDto: StandardListDto,
        private fb: FormBuilder) {

            if (standardListDto.standardListName) {
                this.standardListForm.patchValue({
                    standardListName: standardListDto.standardListName,
                });
            }

            if (standardListDto.defaultSentenceLanguage) {
                this.standardListForm.patchValue({
                    defaultSentenceLanguage: standardListDto.defaultSentenceLanguage,
                });
            }

            if (standardListDto.defaultWordLanguage) {
                this.standardListForm.patchValue({
                    defaultWordLanguage: standardListDto.defaultWordLanguage,
                });
            }
    }

    public getDto(): StandardListDto {
        this.standardListDto.standardListName = this.standardListForm.value.standardListName;
        this.standardListDto.defaultSentenceLanguage = this.standardListForm.value.defaultSentenceLanguage;
        this.standardListDto.defaultWordLanguage = this.standardListForm.value.defaultWordLanguage;
        return this.standardListDto;
    }
}
