import { Component, Injector, OnInit } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { AuctionDto, AuctionServiceProxy, ProductDto, ProductServiceProxy } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Paginator } from "primeng/paginator";
import { Table } from "primeng/table";
import { finalize } from "rxjs";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { fromByteArray } from "base64-js";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  templateUrl: "./view-auction.component.html",
  animations: [appModuleAnimation()],
})
export class ViewAuctionComponent extends AppComponentBase implements OnInit {
  productId: number;
  bid: number;
  userBids = [];
  auction: AuctionDto = new AuctionDto();
  product: ProductDto = new ProductDto();
  auctionBidtimeOut: NodeJS.Timeout;
  constructor(
    injector: Injector,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _modalService: BsModalService,
    private _auctionServiceProxy: AuctionServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.productId = parseInt(this._activatedRoute.snapshot.queryParams['id']);
    this.getAllRecord();
  }
  getAllRecord(event?) {
    if (this.productId) {
      this._auctionServiceProxy.getAllAuctionProduct(this.productId, undefined, undefined,
        event ? (event?.first ? event?.first : 0) : 0,
        event ? event?.rows : 10).subscribe((result) => {
          if (result.items.length) {
            this.product = result.items[0]
            this.getAllProductBids();
          }
          else {
            this.message.error("Product not exist");
            this._router.navigate(['/app/Auctions']);
          }
        })
    }
    else {
      this.message.error("Product not exist");
      this._router.navigate(['/app/Auctions']);
    }
  }
  getAllProductBids() {
    this.auctionBidtimeOut = setInterval(() => {
      this._auctionServiceProxy.getAllProducttBid(this.productId).subscribe((result) => {
        this.userBids = result.sort((a, b) => b.bid - a.bid);
      })
    }, 2000);
  }
  addUserBid() {
    if (this.bid > this.product.basePrice) {
      this._auctionServiceProxy.addUserBid(this.productId, this.bid).subscribe((result) => {
        this.bid = null;
        if (!result) {
          this.message.error("Auction time over");
          this._router.navigate(['/app/Auctions']);
        }
      })
    }
    else {
      this.message.info("Please Add higher bid");
    }
  }
}