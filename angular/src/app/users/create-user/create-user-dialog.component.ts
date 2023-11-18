import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { forEach as _forEach, map as _map } from "lodash-es";
import { AppComponentBase } from "@shared/app-component-base";
import {
  UserServiceProxy,
  CreateUserDto,
  RoleDto,
  LocationServiceProxy,
  CommonLookUpServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { AbpValidationError } from "@shared/components/validation/abp-validation.api";

@Component({
  templateUrl: "./create-user-dialog.component.html",
})
export class CreateUserDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  user = new CreateUserDto();
  roles: RoleDto[] = [];
  checkedRolesMap: { [key: string]: boolean } = {};
  defaultRoleCheckedStatus = false;
  passwordValidationErrors: Partial<AbpValidationError>[] = [
    {
      name: "pattern",
      localizationKey:
        "PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber",
    },
  ];
  confirmPasswordValidationErrors: Partial<AbpValidationError>[] = [
    {
      name: "validateEqual",
      localizationKey: "PasswordsDoNotMatch",
    },
  ];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _commonLookUpServiceProxy: CommonLookUpServiceProxy,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef,
    private _locationServiceProxy: LocationServiceProxy
  ) {
    super(injector);
  }
  locationlist: any;
  location: any;
  userTypeList=[{name:"Seller", value:1},{name:"Buyer", value:0}]
  userType:number;

  ngOnInit(): void {
    this.user.isActive = true;

    this._userService.getRoles().subscribe((result) => {
      this.roles = result.items;
      this.setInitialRolesStatus();
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
    // just return default role checked status
    // it's better to use a setting
    return this.defaultRoleCheckedStatus;
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

    this._userService.create(this.user).subscribe(
      () => {
        this.notify.info(this.l("SavedSuccessfully"));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
