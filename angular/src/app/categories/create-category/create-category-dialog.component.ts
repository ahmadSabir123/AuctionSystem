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
  TenantServiceProxy,
  CategoryServiceProxy,
  CategoryDto
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'createCategoryModal',
  templateUrl: './create-category-dialog.component.html'
})
export class CreateCategoryDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  category: CategoryDto = new CategoryDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _categoryServiceProxy: CategoryServiceProxy,
    public _tenantService: TenantServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {

  }

  save(): void {
    this.saving = true;

    this._categoryServiceProxy.createOrEdit(this.category).subscribe(()=>{
      this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.saving = false;
        this.onSave.emit(); })
  }
}
