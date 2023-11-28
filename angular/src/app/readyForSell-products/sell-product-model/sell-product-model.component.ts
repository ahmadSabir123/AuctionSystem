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
  AuctionDto,
  AuctionServiceProxy,
  TenantServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'sellAuctionModelModel',
  templateUrl: './sell-product-model.component.html'
})
export class SellProductModelComponent extends AppComponentBase
  implements OnInit {
  prouctImg: string
  saving = false;
  productId: number;
  auction: AuctionDto = new AuctionDto();
  modelTitle: string;
  @Output() onSave = new EventEmitter<any>();


  constructor(
    injector: Injector,
    private _commonLookUpServiceProxy: CommonLookUpServiceProxy,
    private _auctionServiceProxy: AuctionServiceProxy,
    public _locationServiceProxy: LocationServiceProxy,
    public _categoryServiceProxy: CategoryServiceProxy,
    public _tenantService: TenantServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.auction = new AuctionDto();
    if (this.productId) {
      this._commonLookUpServiceProxy.getHigherBidUserDetail(this.productId).subscribe((result) => {
        this.auction = result;
      })
    }
    else {
      this.prouctImg = 'assets/img/upload.png';
    }
    this.modelTitle = 'Sell Product';
  }
  sellProduct() {
    abp.message.confirm(
      this.l('Are You Sure to sell ' + this.auction.productName + " " + "To" + " " + this.auction.userName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._commonLookUpServiceProxy.sellProduct(this.productId).subscribe(() => {
            abp.notify.success(this.l('Success fully Sell'));
            this.bsModalRef.hide();
            this.saving = false;
            this.onSave.emit();
          });
        }
      }
    );
  }

}
