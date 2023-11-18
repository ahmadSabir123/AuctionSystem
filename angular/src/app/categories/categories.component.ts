import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs';
import { CreateCategoryDialogComponent } from './create-category/create-category-dialog.component';
import { EditCategoryDialogComponent } from './edit-category/edit-category-dialog.component';
import { CategoryServiceProxy } from '@shared/service-proxies/service-proxies';



@Component({
    templateUrl: './categories.component.html',
    animations: [appModuleAnimation()]
  })

  export class CategoriesComponent extends AppComponentBase implements OnInit  {
    @ViewChild("dataTable", { static: true }) dataTable: Table;
    @ViewChild("paginator", { static: true }) paginator: Paginator;
  
    categoryRecord = [];
    filter: any;
    totalRecord: number;
    constructor(
      injector: Injector,
      private _modalService: BsModalService,
      private _categoryServiceProxy: CategoryServiceProxy
    ) {
      super(injector);
    }
    ngOnInit(): void {}
    getAllRecord(event?: LazyLoadEvent) {
      this._categoryServiceProxy
        .getAllCategories(
          this.filter,undefined,
          undefined,
          this.getSkipCount(this.paginator, event),
          this.getMaxResultCount(this.paginator, event)
        )
        .subscribe((result) => {
          this.categoryRecord = result.items;
          this.totalRecord = result.totalCount;
        });
    }
  
    createOrEditCategory(id?) {
      let createOrEditcategoryDialog: BsModalRef;
      if (!id) {
        createOrEditcategoryDialog = this._modalService.show(
          CreateCategoryDialogComponent,
          {
            class: 'modal-lg',
          }
        );
      } else {
        createOrEditcategoryDialog = this._modalService.show(
          EditCategoryDialogComponent,
          {
            class: 'modal-lg',
            initialState: {
              id: id,
            },
          }
        );
      }
      createOrEditcategoryDialog.content.onSave.subscribe(() => {
        this.getAllRecord();
      });
    }
  
    deleteCategory(id) {
      abp.message.confirm(
        this.l("Are You Sure", ""),
        undefined,
        (result: boolean) => {
          if (result) {
            this._categoryServiceProxy
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
