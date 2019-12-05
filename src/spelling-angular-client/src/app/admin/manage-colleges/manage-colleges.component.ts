import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { CollegeClient, CollegeDto } from 'src/app/core/services/clients';

@Component({
  selector: 'app-manage-colleges',
  templateUrl: './manage-colleges.component.html',
  styleUrls: ['./manage-colleges.component.scss']
})
export class ManageCollegesComponent implements OnInit {
  displayedColumns = ['collegeName'];
  dataSource = new MatTableDataSource<CollegeDto>();
  colleges: Array<CollegeDto>;

  constructor(
    private _collegeClient: CollegeClient,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this._collegeClient.getAllColleges()
    .subscribe(
      (data) => {
        this.colleges = data;
        this.dataSource.data = data;
      }
    );
  }

}
