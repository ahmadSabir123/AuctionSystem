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
  CategoryDto,
  CategoryServiceProxy,
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'editCategoryModal',
  templateUrl: './edit-category-dialog.component.html'
})
export class EditCategoryDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  category: CategoryDto = new CategoryDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _categoryServiceProxy: CategoryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._categoryServiceProxy.getAllCategories(undefined,this.id,undefined,undefined,undefined).subscribe((result) => {
      this.category = result.items[0];
    });
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
