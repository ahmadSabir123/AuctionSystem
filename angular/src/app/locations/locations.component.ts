import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';



@Component({
    templateUrl: './locations.component.html',
    animations: [appModuleAnimation()]
  })

  export class LocationsComponent extends AppComponentBase implements OnInit {
    constructor(
        injector: Injector,
      ) {
        super(injector);
      }
      ngOnInit(): void {
        throw new Error('Method not implemented.');
    }
    createLocation(){
        
    }

  }
