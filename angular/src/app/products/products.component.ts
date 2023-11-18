import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { ProductServiceProxy } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Paginator } from "primeng/paginator";
import { Table } from "primeng/table";
import { finalize } from "rxjs";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateOrEditProductModelComponent } from "./create-or-edit-product/create-or-edit-product.component";


@Component({
  templateUrl: "./products.component.html",
  animations: [appModuleAnimation()],
})
export class ProductComponent extends AppComponentBase implements OnInit {
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;

  productRecord = [];
  filter: any;
  totalRecord: number;
  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _productServiceProxy: ProductServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {}
  getAllRecord(event?: LazyLoadEvent) {
    this._productServiceProxy
      .getAllProduct(
        this.filter,undefined,
        undefined,
        this.getSkipCount(this.paginator, event),
        this.getMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.productRecord = result.items;
        this.totalRecord = result.totalCount;
      });
  }

  createOrEditProduct(id?) {
    let createOrEditProductDialog: BsModalRef;
      createOrEditProductDialog = this._modalService.show(
        CreateOrEditProductModelComponent,
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

  deleteProduct(id) {
    abp.message.confirm(
      this.l("Are You Sure", ""),
      undefined,
      (result: boolean) => {
        if (result) {
          this._productServiceProxy
            .delete(id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l("SuccessfullyDeleted"));
                this.getAllRecord();
              })
            )
            .subscribe((result) => {});
        }
      }
    );
  }
}
