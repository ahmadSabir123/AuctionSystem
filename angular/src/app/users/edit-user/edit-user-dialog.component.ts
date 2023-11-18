import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  UserDto,
  RoleDto,
  LocationServiceProxy,
  CommonLookUpServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './edit-user-dialog.component.html'
})
export class EditUserDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  user = new UserDto();
  roles: RoleDto[] = [];
  checkedRolesMap: { [key: string]: boolean } = {};
  id: number;

  @Output() onSave = new EventEmitter<any>();
  locationlist: any;
  location: any;
  userTypeList = [{ name: "Seller", value: 1 }, { name: "Buyer", value: 0 }]
  userType: number;

  constructor(
    injector: Injector,
    private _commonLookUpServiceProxy: CommonLookUpServiceProxy,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private _locationServiceProxy: LocationServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._userService.get(this.id).subscribe((result) => {
      this.user = result;
      this.userType = this.user.type;
      this.location = this.user.locationId;

      this._userService.getRoles().subscribe((result2) => {
        this.roles = result2.items;
        this.setInitialRolesStatus();
      });
    });
    if (this.isGranted("Pages.Locations")) {
      this.getAllLocationForDropdown();
    }
  }
  getAllLocationForDropdown() {
    this._commonLookUpServiceProxy
      .getAllLocationsForDropdown()
      .subscribe((result) => {
        this.locationlist = result;
      });
  }

  setInitialRolesStatus(): void {
    _map(this.roles, (item) => {
      this.checkedRolesMap[item.normalizedName] = this.isRoleChecked(
        item.normalizedName
      );
    });
  }

  isRoleChecked(normalizedName: string): boolean {
    return _includes(this.user.roleNames, normalizedName);
  }

  onRoleChange(role: RoleDto, $event) {
    this.checkedRolesMap[role.normalizedName] = $event.target.checked;
  }

  getCheckedRoles(): string[] {
    const roles: string[] = [];
    _forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        roles.push(key);
      }
    });
    return roles;
  }

  save(): void {
    this.saving = true;

    this.user.roleNames = this.getCheckedRoles();
    this.user.locationId = this.location;
    this.user.type = this.userType;

    this._userService.update(this.user).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
