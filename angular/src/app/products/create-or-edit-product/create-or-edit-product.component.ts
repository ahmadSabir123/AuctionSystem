import {
  Component,
  Injector,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CategoryDto,
  CategoryServiceProxy,
  CommonLookUpServiceProxy,
  CreateTenantDto,
  LocationServiceProxy,
  ProductDto,
  ProductServiceProxy,
  TenantServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'createOrEditProductModel',
  templateUrl: './create-or-edit-product.component.html'
})
export class CreateOrEditProductModelComponent extends AppComponentBase
  implements OnInit {
  prouctImg: string
  locationlist = [];
  location: number;
  catagory: number;
  saving = false;
  productId: number;
  product: ProductDto = new ProductDto();
  modelTitle: string;
  @Output() onSave = new EventEmitter<any>();
  categorieslist: CategoryDto[];
  startDate: any;
  endDate: any;

  constructor(
    injector: Injector,
    private _commonLookUpServiceProxy: CommonLookUpServiceProxy,
    private _productServiceProxy: ProductServiceProxy,
    public _locationServiceProxy: LocationServiceProxy,
    public _categoryServiceProxy: CategoryServiceProxy,
    public _tenantService: TenantServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.product = new ProductDto();
    if (this.productId) {
      this._productServiceProxy.getProductForEdit(this.productId).subscribe((result) => {
        this.product = result;
        this.startDate = result.auctionStartAt.toDate();
        this.endDate = result.auctionEndAt.toDate();
        this.location = this.product.locationId;
        this.catagory = this.product.categoryId;
        this.prouctImg = this.getFileType(result.imageBase64);
      })
    }
    else {
      this.prouctImg = 'assets/img/upload.png';
    }
    this.modelTitle = this.productId ? "Edit Product" : "Create Product";
    this.getAllLocationForDropdown();
    this.getAllCategoriesForDropdown();
  }
  changeFile(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });
  }
  onUpload(event: any) {
    const file: File = event.target.files[0];
    this.changeFile(file).then((base64: string): any => {
      this.prouctImg = base64;
      this.product.imageBase64 = base64.split('base64,')[1];
    });
  }
  getAllLocationForDropdown() {
    this._commonLookUpServiceProxy
      .getAllLocationsForDropdown()
      .subscribe((result) => {
        this.locationlist = result;
      });
  }
  getAllCategoriesForDropdown() {
    this._commonLookUpServiceProxy
      .getAllCategoriesForDropdown()
      .subscribe((result) => {
        ;
        this.categorieslist = result;
      });
  }

  save(): void {
    this.product.auctionStartAt = moment(this.startDate).utc(true);
    this.product.auctionEndAt = moment(this.endDate).utc(true);
    this.product.locationId = this.location;
    this.product.categoryId = this.catagory;
    this.saving = true;
    this._productServiceProxy.createOrEdit(this.product).subscribe(() => {
      this.notify.info(this.l('SavedSuccessfully'));
      this.bsModalRef.hide();
      this.saving = false;
      this.onSave.emit();
    })
  }
}
