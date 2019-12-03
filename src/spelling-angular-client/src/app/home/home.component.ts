import { Component, OnInit } from '@angular/core';
import { SpeachClient, TestClient } from '../core/services/clients';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    constructor(
        private readonly speachClient: SpeachClient,
        private readonly testClient: TestClient
    ) { }

    ngOnInit() {

        
    }

    playSound() {
        this.testClient.getSomethigs('test').subscribe(
            (result) => {
                debugger;
                const test = result.rowVersion;
            }
        )
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