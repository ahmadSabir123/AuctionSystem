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
  TenantServiceProxy,
  TenantDto,
  LocationServiceProxy,
  LocationDto
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'editLocationModal',
  templateUrl: 'edit-location-dialog.component.html'
})
export class EditLocationDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  location: LocationDto = new LocationDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _locationServiceProxy: LocationServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._locationServiceProxy.getAllLocation(undefined,this.id,undefined,undefined,undefined).subscribe((result) => {
      this.location = result.items[0];
    });
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
