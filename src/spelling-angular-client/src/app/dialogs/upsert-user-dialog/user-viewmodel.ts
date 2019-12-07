import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddUserDto, UpdateUserDto } from 'src/app/core/services/clients';

export class UserViewmodel {

    userForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        roleId: ['', Validators.required],
        email: ['', [Validators.email, Validators.required]],
        collegeIds: [[]]
      });


    constructor(private userDto: AddUserDto | UpdateUserDto, private fb: FormBuilder) {

        if (userDto.collegeIds) {
            this.userForm.patchValue({
                collegeIds: userDto.collegeIds,
            });
        }
        if (userDto.firstName) {
            this.userForm.patchValue({
                firstName: userDto.firstName,
            });
        }
        if (userDto.lastName) {
            this.userForm.patchValue({
                lastName: userDto.lastName,
            });
        }
        if (userDto.roleId) {
            this.userForm.patchValue({
                roleId: userDto.roleId,
            });
        }
        if (userDto.email) {
            this.userForm.patchValue({
                email: userDto.email,
            });
        }
    }

    public getDto(): AddUserDto | UpdateUserDto {
        this.userDto.collegeIds = this.userForm.value.collegeIds;
        this.userDto.email = this.userForm.value.email;
        this.userDto.firstName = this.userForm.value.firstName;
        this.userDto.lastName = this.userForm.value.lastName;
        this.userDto.roleId = this.userForm.value.roleId;
        return this.userDto;
    }
}
