import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserDto, UpdateUserDto } from 'src/app/core/services/clients';

export class UserViewmodel {
    userForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.required]],
        collegeIds: [[], Validators.required]
      });


    constructor(userDto: AddUserDto | UpdateUserDto, private fb: FormBuilder) {

        debugger;
        if (userDto.email) {
            this.userForm.patchValue({
                email: [userDto.email],
            });
        }

        // this.userForm.setValue({
        //     email: [userDto.email],
        //     firstName: [userDto.firstName, Validators.required],
        //     lastName: userDto.lastName,
        //     collegeIds: userDto.collegeIds
        // }
        // );
    }
}
