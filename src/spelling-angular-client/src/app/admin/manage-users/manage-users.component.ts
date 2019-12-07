import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
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
  editedUser: UserDto;
  error: string;

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

    debugger;
    if (userdto instanceof AddUserDto) {
      debugger;
    } else if (userdto instanceof UpdateUserDto) {
      debugger;
    } else {
      debugger;
    }

    const dialogRef = this.dialog.open(UpsertUserDialogComponent, {
      width: '348px',
      data: userdto
    });

    dialogRef.afterClosed().subscribe(dialogObject => {
      const typedObject = dialogObject as AddUserDto | UpdateUserDto;
      if (typedObject) {

        if (typedObject instanceof AddUserDto) {

        } else if (typedObject instanceof UpdateUserDto) {

        }

      //   const dto: NameOnlyUpsertDto = new NameOnlyUpsertDto();
      //   dto.id = typedObject.id;
      //   dto.name = typedObject.userName;
      //   dto.rowVersion = typedObject.rowVersion;
      //   if (typedObject.id) {
      //     this.doUpdate(dto, typedObject);
      //   } else  {
      //     this.doAdd(dto);
      //   }
      }
    });
  }

  private doUpdate(dto: UpdateUserDto, user: UserDto): void {
    this._userClient.updateUser(dto)
    .subscribe(
      (savedUser) => {
        // this.editedUser.userName = user.userName;
        // this.editedUser.rowVersion = savedUser.rowVersion;
       }
      , error => this.error = Utils.formatError(error)
    );
  }

  private doAdd(dto: AddUserDto): void {
    this._userClient.addUser(dto)
        .subscribe(
          (savedUser) => {
            // const newUser: UserDto = new UserDto({
            //     userName: dto.name,
            //     id: savedUser.id,
            //     rowVersion: savedUser.rowVersion,
            //     createdAt: null,
            //     createdByAppUserId: null
            //   }
            // );

            // let data = this.dataSource.data;
            // data.push(newUser);
            // data = data.sort(Utils.compareUserName);
            // this.dataSource.data = data;
          }
          , error => this.error = Utils.formatError(error)
        );
  }
}
