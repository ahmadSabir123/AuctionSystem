import {
  Component,
  Injector,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CreateTenantDto,
  LocationDto,
  LocationServiceProxy,
  TenantServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'createLocationModal',
  templateUrl: 'create-location-dialog.component.html'
})
export class CreateLocationDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  location: LocationDto = new LocationDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _locationServiceProxy: LocationServiceProxy,
    public _tenantService: TenantServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {

  }

  save(): void {
    this.saving = true;

    this._locationServiceProxy.createOrEdit(this.location).subscribe(()=>{
      this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.saving = false;
        this.onSave.emit(); })
  }
}
