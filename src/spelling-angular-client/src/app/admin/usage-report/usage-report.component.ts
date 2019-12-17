import { Component, OnInit } from '@angular/core';
import { UserClient, CollegeUsage, YearClassClient, LookupDto } from 'src/app/core/services/clients';

@Component({
  selector: 'app-usage-report',
  templateUrl: './usage-report.component.html',
  styleUrls: ['./usage-report.component.scss']
})
export class UsageReportComponent implements OnInit {

  reportData: Array<CollegeUsage> = [];
  collegeOptions: Array<LookupDto> = [];
  selectedCollege: LookupDto;

  constructor(
    private _userClient: UserClient,
    private _yearClassClient: YearClassClient) {

     }

  ngOnInit() {

    this._yearClassClient.getCollegeLookups()
    .subscribe(
      (data) => {
        this.collegeOptions = data;
        if (data.length === 1) {
          this.selectedCollege = data[0];
          this.collegeSelected();
        }
      }
    );
  }

  collegeSelected() {
    this._userClient.getUsageReport(this.selectedCollege.id).subscribe
    (
      (data) => {
        this.reportData = data;
      }
    );
  }
}
