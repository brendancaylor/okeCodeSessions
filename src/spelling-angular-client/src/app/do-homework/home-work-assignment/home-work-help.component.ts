import {Component} from '@angular/core';
import {MatBottomSheet, MatBottomSheetRef} from '@angular/material/bottom-sheet';

@Component({
    selector: 'app-home-work-help',
    templateUrl: 'home-work-help.component.html',
  })

export class HomeWorkHelpComponent {
    constructor(private _bottomSheetRef: MatBottomSheetRef<HomeWorkHelpComponent>) {}

}
