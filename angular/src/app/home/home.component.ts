import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CommonLookUpServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
})
export class HomeComponent extends AppComponentBase implements OnInit {
  constructor(injector: Injector, private _commonLookUpServiceProxy: CommonLookUpServiceProxy) {
    super(injector);
  }
  totalProduct = 0
  buyProductsCount = 0
  soldProductsCount = 0
  readyForSaleProductsCount = 0
  ngOnInit(): void {
    this._commonLookUpServiceProxy.getAllProductCount().subscribe((result) => {debugger
      this.totalProduct = result;
    })
    this._commonLookUpServiceProxy.getAllBuyProductsCount().subscribe((result) => {debugger
      this.buyProductsCount = result;
    })
    this._commonLookUpServiceProxy.getAllSoldProductsCount().subscribe((result) => {debugger
      this.soldProductsCount = result;
    })
    this._commonLookUpServiceProxy.getAllReadyForSaleProductsCount().subscribe((result) => {debugger
      this.readyForSaleProductsCount = result;
    })

  }
}
