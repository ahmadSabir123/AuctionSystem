import { Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { AuctionDto, AuctionServiceProxy, CommonLookUpServiceProxy, ProductDto, ProductServiceProxy } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Paginator } from "primeng/paginator";
import { Table } from "primeng/table";
import { finalize } from "rxjs";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { fromByteArray } from "base64-js";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  templateUrl: "./buy-products.component.html",
  animations: [appModuleAnimation()],
})
export class BuyProductComponent extends AppComponentBase implements OnInit {
  productRecord = [];
  filter: any;
  totalRecord: number;
  constructor(
    injector: Injector,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _modalService: BsModalService,
    private _auctionServiceProxy: AuctionServiceProxy,
    private _commonLookUpServiceProxy: CommonLookUpServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllRecord();
  }

  getAllRecord(event?: LazyLoadEvent) {
    this._commonLookUpServiceProxy.getAllBuyProducts(undefined, this.filter,
      undefined,
      event ? (event?.first ? event?.first : 0) : 0,
      event ? event?.rows : 10).subscribe((result) => {
        this.productRecord = result.items;
        this.totalRecord = result.totalCount;
      })
  }
}