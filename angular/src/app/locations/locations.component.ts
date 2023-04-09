import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { LocationServiceProxy } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';



@Component({
  templateUrl: './locations.component.html',
  animations: [appModuleAnimation()]
})

export class LocationsComponent extends AppComponentBase implements OnInit {
  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('paginator', { static: true }) paginator: Paginator;

  locationRecord= []
  filter: any
  totalRecord: number;
  constructor(
    injector: Injector,
    private _locationServiceProxy: LocationServiceProxy,
  ) {
    super(injector);
  }
  ngOnInit(): void {
  }
  getAllRecord(event?: LazyLoadEvent) {
    this._locationServiceProxy.getAllLocation(this.filter, undefined, this.getSkipCount(this.paginator, event), this.getMaxResultCount(this.paginator, event)).subscribe((result) => {
      this.locationRecord = result.items;
      this.totalRecord = result.totalCount
    })
  }
  createLocation() {

  }
  deleteLocation(event) {

  }

}
