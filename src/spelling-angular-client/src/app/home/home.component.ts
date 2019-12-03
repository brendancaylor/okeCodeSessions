import { Component, OnInit } from '@angular/core';
import { SpeachClient, TestClient, CollegeClient, NameOnlyUpsertDto } from '../core/services/clients';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    constructor(
        private readonly speachClient: SpeachClient,
        private readonly testClient: TestClient,
        private readonly collegeClient: CollegeClient
    ) { }

    ngOnInit() {

    }

    playSound() {

        this.collegeClient.getCollege('2d0e4f35-df1e-4c77-9569-786f44b3acb6')
        .subscribe(
            (college) => {
                const upsertDto: NameOnlyUpsertDto = new NameOnlyUpsertDto();
                upsertDto.id = college.id;
                upsertDto.name = 'Test 2';
                upsertDto.rowVersion = college.rowVersion;

                this.collegeClient.updateCollege(upsertDto).subscribe(
                    (updatedCollege) => {

                    }
                );
            }
        );


        // const dto: AddCollegeDto = new AddCollegeDto();
        // dto.collegeName = "Test College";
        // this.collegeClient.addCollege(dto).subscribe(
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
