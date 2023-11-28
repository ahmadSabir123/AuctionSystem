import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { AuctionServiceProxy, CommonLookUpServiceProxy, ProductServiceProxy } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Paginator } from "primeng/paginator";
import { Table } from "primeng/table";
import { finalize } from "rxjs";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { fromByteArray } from "base64-js";
import { Router } from "@angular/router";
import { SellProductModelComponent } from "./sell-product-model/sell-product-model.component";


@Component({
  templateUrl: "./readyForSell-products.component.html",
  animations: [appModuleAnimation()],
})
export class ReadyForSellProductComponent extends AppComponentBase implements OnInit {
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;

  productRecord = [];
  filter: any;
  totalRecord: number;
  constructor(
    injector: Injector,
    private _router: Router,
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
    this._commonLookUpServiceProxy.getAllReadyForSaleProducts(undefined, this.filter,
      undefined,
      event ? (event?.first ? event?.first : 0) : 0,
      event ? event?.rows : 10).subscribe((result) => {
        this.productRecord = result.items;
        this.totalRecord = result.totalCount;
      })
  }
  open(id) {
    let createOrEditProductDialog: BsModalRef;
    createOrEditProductDialog = this._modalService.show(
      SellProductModelComponent,
      {
        class: 'modal-lg',
        initialState: {
          productId: id,
        },
      }
    );

    createOrEditProductDialog.content.onSave.subscribe(() => {
      this.getAllRecord();
    });
  }
}
