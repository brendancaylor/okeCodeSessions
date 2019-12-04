import { Component, OnInit } from '@angular/core';
import {
    SpeachClient,
    TestClient,
    CollegeClient,
    NameOnlyUpsertDto,
    UserClient,
    AddUserDto
} from '../core/services/clients';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    constructor(
        private readonly speachClient: SpeachClient,
        private readonly testClient: TestClient,
        private readonly collegeClient: CollegeClient,
        private readonly userClient: UserClient
    ) { }

    ngOnInit() {

    }

    playSound() {

        const addUserDto: AddUserDto = new AddUserDto();
        addUserDto.email = 'test@builditloveit.com';
        addUserDto.firstName = 'firstName';
        addUserDto.lastName = 'lastName';
        addUserDto.roleId = '6ed6abf8-8e4e-45c4-ad90-0987906e75a4';
        this.userClient.addUser(addUserDto).subscribe(
            (test) => {
                debugger;
            }
        );

        // this.collegeClient.getCollege('baf9c688-d079-4a89-8fd7-8754b973174c')
        // .subscribe(
        //     (college) => {
        //         const upsertDto: NameOnlyUpsertDto = new NameOnlyUpsertDto();
        //         upsertDto.id = college.id;
        //         upsertDto.name = 'Test 2';
        //         upsertDto.rowVersion = college.rowVersion;

        //         this.collegeClient.updateCollege(upsertDto).subscribe(
        //             (updatedCollege) => {

        //             }
        //         );
        //     }
        // );

        // const upsertDto: NameOnlyUpsertDto = new NameOnlyUpsertDto();
        // upsertDto.name = 'College 1';
        // this.collegeClient.addCollege(upsertDto).subscribe(
        //     (result) => {
        //         debugger;
        //     }
        // );

        // this.testClient.getSomethigs('test').subscribe(
        //     (result) => {
        //         debugger;
        //         const test = result.rowVersion;
        //     }
        // )


        // this.speachClient.getSpeach('Home page').subscribe(
        //     (blob) => {
        //         var blobUrl = URL.createObjectURL(blob);
        //         let audio = new Audio();
        //         audio.src = blobUrl;
        //         audio.load();
        //         audio.play();
        //     }
        // );
    }
}
