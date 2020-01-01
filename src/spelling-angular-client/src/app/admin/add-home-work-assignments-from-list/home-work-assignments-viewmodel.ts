import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeWorkAssignmentAddDto, HomeWorkAssignmentUpdateDto, StandardListItemDto } from 'src/app/core/services/clients';
import * as moment from 'moment';

export class HomeWorkAssignmentsViewmodel {

    homeWorkAssignmentForm = this.fb.group({
        dueDate: [moment(), [Validators.required]],
        wordsPerAssignment: [10, [Validators.required]]
      });

    homeWorkAssignmentSplits: Array<HomeWorkAssignmentSplit> = [];

    constructor(private fb: FormBuilder) {
    }

}

export class HomeWorkAssignmentSplit {
    dueDate: moment.Moment = moment();
    standardListItems: Array<StandardListItemDto> = [];
}
