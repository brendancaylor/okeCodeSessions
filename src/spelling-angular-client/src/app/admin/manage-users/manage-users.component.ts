import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { UserClient, UserDto, NameOnlyUpsertDto, AddUserDto, UpdateUserDto } from 'src/app/core/services/clients';
import { Utils } from 'src/app/core/utils';
import { UpsertUserDialogComponent } from 'src/app/dialogs/upsert-user-dialog/upsert-user-dialog.component';

@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.scss']
})
export class ManageUsersComponent implements OnInit {
  displayedColumns = ['firstName', 'lastName', 'email', 'actions'];
  dataSource = new MatTableDataSource<UserDto>();
  editedUser: UserDto | null = null;
  error: string | null = null;

  constructor(
    private _userClient: UserClient,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers(): void {
    this._userClient.getAllUsers()
    .subscribe(
      (data) => {
        this.dataSource.data = data;
      }
    );
  }

  addUser(): void {
    const userToAdd: AddUserDto = new AddUserDto();
    this.setupDialog(userToAdd);
  }

  editUser(user: UserDto): void {
    this.editedUser = user;
    const userToEdit: UpdateUserDto = new UpdateUserDto();
    Object.assign(userToEdit, user);
    this.setupDialog(userToEdit);
  }


  private setupDialog(userdto: AddUserDto | UpdateUserDto): void {
    const dialogRef = this.dialog.open(UpsertUserDialogComponent, {
      width: '348px',
      data: userdto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as AddUserDto | UpdateUserDto;
      if (typedObject) {
        if (typedObject instanceof AddUserDto) {
          this.doAdd(typedObject);
        } else if (typedObject instanceof UpdateUserDto) {
          this.doUpdate(typedObject);
        }
      }
    });
  }

  private doUpdate(dto: UpdateUserDto): void {
    this._userClient.updateUser(dto)
    .subscribe(
      (savedUser) => {
        this.loadUsers();
       }
      , error => this.error = Utils.formatError(error)
    );
  }

  private doAdd(dto: AddUserDto): void {
    this._userClient.addUser(dto)
        .subscribe(
          (savedUser) => {
            this.loadUsers();
          }
          , error => this.error = Utils.formatError(error)
        );
  }
}
