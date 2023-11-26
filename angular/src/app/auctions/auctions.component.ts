import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { AuctionServiceProxy, ProductServiceProxy } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Paginator } from "primeng/paginator";
import { Table } from "primeng/table";
import { finalize } from "rxjs";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { fromByteArray } from "base64-js";
import { Router } from "@angular/router";


@Component({
  templateUrl: "./auctions.component.html",
  animations: [appModuleAnimation()],
})
export class AuctionComponent extends AppComponentBase implements OnInit {
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;

  productRecord = [];
  filter: any;
  totalRecord: number;
  constructor(
    injector: Injector,
    private _router: Router,
    private _modalService: BsModalService,
    private _auctionServiceProxy: AuctionServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllRecord();
  }

  getAllRecord(event?: LazyLoadEvent) {
    this._auctionServiceProxy.getAllAuctionProduct(undefined, this.filter,
      undefined,
      event ? (event?.first ? event?.first : 0) : 0,
      event ? event?.rows : 10).subscribe((result) => {
        this.productRecord = result.items;
        this.totalRecord = result.totalCount;
      })
  }
  navigateToAuctionViewScreen(id) {
    this._router.navigate(['/app/viewAuctions'], { queryParams: { id: id } });
  }
}
