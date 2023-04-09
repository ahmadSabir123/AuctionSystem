import { Component, Injector, OnInit, ViewChild } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { AppComponentBase } from "@shared/app-component-base";
import { LocationServiceProxy } from "@shared/service-proxies/service-proxies";
import { LazyLoadEvent } from "primeng/api";
import { Paginator } from "primeng/paginator";
import { Table } from "primeng/table";
import { finalize } from "rxjs";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateLocationDialogComponent } from "./create-location/create-location-dialog.component";
import { EditLocationDialogComponent } from "./edit-location/edit-location-dialog.component";


@Component({
  templateUrl: "./locations.component.html",
  animations: [appModuleAnimation()],
})
export class LocationsComponent extends AppComponentBase implements OnInit {
  @ViewChild("dataTable", { static: true }) dataTable: Table;
  @ViewChild("paginator", { static: true }) paginator: Paginator;

  locationRecord = [];
  filter: any;
  totalRecord: number;
  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _locationServiceProxy: LocationServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {}
  getAllRecord(event?: LazyLoadEvent) {
    this._locationServiceProxy
      .getAllLocation(
        this.filter,undefined,
        undefined,
        this.getSkipCount(this.paginator, event),
        this.getMaxResultCount(this.paginator, event)
      )
      .subscribe((result) => {
        this.locationRecord = result.items;
        this.totalRecord = result.totalCount;
      });
  }

  createOrEditLocation(id?) {
    let createOrEditLocationDialog: BsModalRef;
    if (!id) {
      createOrEditLocationDialog = this._modalService.show(
        CreateLocationDialogComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditLocationDialog = this._modalService.show(
        EditLocationDialogComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditLocationDialog.content.onSave.subscribe(() => {
      this.getAllRecord();
    });
  }

  deleteLocation(id) {
    abp.message.confirm(
      this.l("Are You Sure", ""),
      undefined,
      (result: boolean) => {
        if (result) {
          this._locationServiceProxy
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
