import { Component, OnInit } from '@angular/core';
import { SpeachClient } from '../core/services/clients';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    constructor(
        private readonly speachClient: SpeachClient
    ) { }

    ngOnInit() {

        
    }

    playSound() {
        debugger;
        this.speachClient.getSpeach('Home page').subscribe(
            (blob) => {
                debugger;
                var blobUrl = URL.createObjectURL(blob);
                let audio = new Audio();
                audio.src = blobUrl;
                audio.load();
                audio.play();
            }
        );
    }
    
}