import { Component, OnInit } from '@angular/core';
import { StandardListDto, StandardListClient, StandardListItemDto } from 'src/app/core/services/clients';
import { MatDialog } from '@angular/material/dialog';
import { UpsertStandardListComponent } from 'src/app/dialogs/upsert-standard-list/upsert-standard-list.component';
import { UpsertGenericWordDialogComponent } from 'src/app/dialogs/upsert-generic-word/upsert-generic-word-dialog.component';
import { HttpClient } from '@angular/common/http';
import { Constants } from 'src/app/constants';
import { WaitingDialogComponent } from 'src/app/dialogs/waiting-dialog/waiting-dialog.component';

@Component({
  selector: 'app-manage-standard-lists',
  templateUrl: './manage-standard-lists.component.html',
  styleUrls: ['./manage-standard-lists.component.scss']
})
export class ManageStandardListsComponent implements OnInit {

  standardListOptions: Array<StandardListDto> = [];
  selectedStandardList!: StandardListDto;

  constructor(
    private _standardListClient: StandardListClient,
    public dialog: MatDialog,
    private readonly httpClient: HttpClient) { }

  ngOnInit() {
    this.loadStandardLists();
  }

  loadStandardLists(): void {
    this._standardListClient.getAllStandardLists()
    .subscribe(
      (data) => {
        this.standardListOptions = data;
        if (data.length === 1) {
          this.selectedStandardList = data[0];
          this.standardListSelected();
        }
      }
    );
  }

  standardListSelected(): void {
    this._standardListClient.getStandardList(this.selectedStandardList.id)
      .subscribe(
        (data) => {
          this.selectedStandardList.standardListItems = data.standardListItems;
        });
  }

  addStandardList(): void {
    const standardListToAdd: StandardListDto = new StandardListDto();
    this.setupStandardListDialog(standardListToAdd);
  }

  editStandardList(standardList: StandardListDto): void {
    const standardListToEdit: StandardListDto = new StandardListDto();
    Object.assign(standardListToEdit, standardList);
    this.setupStandardListDialog(standardListToEdit);
  }


  private setupStandardListDialog(standardListdto: StandardListDto): void {
    const dialogRef = this.dialog.open(UpsertStandardListComponent, {
      width: '348px',
      data: standardListdto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as StandardListDto;
      if (typedObject) {
        if (!typedObject.id) {
          this.doStandardListAdd(typedObject);
        } else {
          this.doStandardListUpdate(typedObject);
        }
      }
    });
  }

  private doStandardListUpdate(dto: StandardListDto): void {
    this._standardListClient.updateStandardList(dto)
    .subscribe(
      (savedStandardList) => {
        this.loadStandardLists();
       }
    );
  }

  private doStandardListAdd(dto: StandardListDto): void {
    this._standardListClient.addStandardList(dto)
        .subscribe(
          (savedStandardList) => {
            this.loadStandardLists();
          }
        );
  }

  addStandardListItem(): void {
    const standardListItemToAdd: StandardListItemDto = new StandardListItemDto();
    standardListItemToAdd.standardListId = this.selectedStandardList.id;
    this.setupStandardListItemDialog(standardListItemToAdd);
  }

  editStandardListItem(standardListItem: StandardListItemDto): void {
    const standardListItemToEdit: StandardListItemDto = new StandardListItemDto();
    Object.assign(standardListItemToEdit, standardListItem);
    this.setupStandardListItemDialog(standardListItemToEdit);
  }


  private setupStandardListItemDialog(standardListItemdto: StandardListItemDto): void {
    const dialogRef = this.dialog.open(UpsertGenericWordDialogComponent, {
      width: '348px',
      data: standardListItemdto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as StandardListItemDto;
      if (typedObject) {
        if (!typedObject.id) {
          this.doStandardListItemAdd(typedObject);
        } else {
          this.doStandardListItemUpdate(typedObject);
        }
      }
    });
  }

  private doStandardListItemUpdate(dto: StandardListItemDto): void {
    this._standardListClient.updateStandardListItem(dto)
    .subscribe(
      (savedStandardListItem) => {
        this.standardListSelected();
       }
    );
  }

  private doStandardListItemAdd(dto: StandardListItemDto): void {
    this._standardListClient.addStandardListItem(dto)
        .subscribe(
          (savedStandardListItem) => {
            this.standardListSelected();
          }
        );
  }

  deleteStandardListItem(standardListItem: StandardListItemDto): void {
    this._standardListClient.deleteStandardListItem(standardListItem.id)
    .subscribe(
      () => {
        const index = this.selectedStandardList!.standardListItems!.indexOf(standardListItem);
        this.selectedStandardList!.standardListItems!.splice(index, 1);
      }
    );
  }

  fileChangeWordItems(event: Event): void {

    const dialogRef = this.dialog.open(WaitingDialogComponent, {
      width: '348px',
      disableClose: true
    });

    const fileList = (event.target as HTMLInputElement).files;
    if (fileList!.length === 0) {
      return;
    }

    const formData = new FormData();

    formData.append('file', fileList![0]);
    const url = `${Constants.apiAutoGeneratedRoot}/api/BatchStandardListItems/${this.selectedStandardList.id}`;

    this.httpClient
      .post(url, formData)
      .subscribe(
        () => {
          this.standardListSelected();
          dialogRef.close();
       },
       (error) => {
         dialogRef.close();
       }
      );

      if (event.target != null) {
        (event.target as HTMLInputElement).value = '';
      }
  }

  fileChangeWordsOnly(event: Event): void {

    const dialogRef = this.dialog.open(WaitingDialogComponent, {
      width: '348px',
      disableClose: true
    });

    const fileList = (event.target as HTMLInputElement).files;
    if (fileList!.length === 0) {
      return;
    }

    const formData = new FormData();

    formData.append('file', fileList![0]);
    const url = `${Constants.apiAutoGeneratedRoot}/api/BatchStandardListItems/words-only/${this.selectedStandardList.id}`;

    this.httpClient
      .post(url, formData)
      .subscribe(
        () => {
          this.standardListSelected();
          dialogRef.close();
       },
       (error) => {
         dialogRef.close();
       }
      );

      if (event.target != null) {
        (event.target as HTMLInputElement).value = '';
      }
  }
}
