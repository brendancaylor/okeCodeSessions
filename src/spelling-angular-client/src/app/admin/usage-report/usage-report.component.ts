import { Component, OnInit } from '@angular/core';
import { UserClient, CollegeUsage } from 'src/app/core/services/clients';

@Component({
  selector: 'app-usage-report',
  templateUrl: './usage-report.component.html',
  styleUrls: ['./usage-report.component.scss']
})
export class UsageReportComponent implements OnInit {

  constructor(private _userClient: UserClient) { }

  reportData: Array<CollegeUsage> = []
  ngOnInit() {
    this._userClient.getUsageReport().subscribe
    (
      (data) => {
        this.reportData = data;
      }
    );
  }

}
