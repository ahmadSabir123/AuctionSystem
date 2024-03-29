import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
import { PaginatorModule } from 'primeng/paginator';
import { DropdownModule } from 'primeng/dropdown';

// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// layout
import { HeaderComponent } from './layout/header.component';
import { HeaderLeftNavbarComponent } from './layout/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header-user-menu.component';
import { FooterComponent } from './layout/footer.component';
import { SidebarComponent } from './layout/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar-menu.component';
import { LocationComponent } from '../app/locations/locations.component';
import { EditLocationDialogComponent } from '../app/locations/edit-location/edit-location-dialog.component';
import { CreateLocationDialogComponent } from '../app/locations/create-location/create-location-dialog.component';
import { CalendarModule } from 'primeng/calendar';
import { CreateOrEditProductModelComponent } from '../app/products/create-or-edit-product/create-or-edit-product.component';
import { ProductComponent } from '../app/products/products.component';
import { CategoryComponent } from '../app/categories/categories.component';
import { CreateCategoryDialogComponent } from './categories/create-category/create-category-dialog.component';
import { EditCategoryDialogComponent } from '../app/categories/edit-category/edit-category-dialog.component';
import { TableModule } from 'primeng/table';
import { AuctionComponent } from './auctions/auctions.component';
import { ViewAuctionComponent } from './auctions/view-auction/view-auction/view-auction.component';
import { InputNumberModule } from 'primeng/inputnumber';
import { SoldProductComponent } from './sold-products/sold-products.component';
import { BuyProductComponent } from './buy-products/buy-products.component';
import { ReadyForSellProductComponent } from './readyForSell-products/readyForSell-products.component';
import { SellProductModelComponent } from './readyForSell-products/sell-product-model/sell-product-model.component';

@NgModule({
    declarations: [
        SellProductModelComponent,
        ReadyForSellProductComponent,
        BuyProductComponent,
        SoldProductComponent,
        ViewAuctionComponent,
        AuctionComponent,
        CreateOrEditProductModelComponent,
        ProductComponent,
        EditCategoryDialogComponent,
        CreateCategoryDialogComponent,
        EditLocationDialogComponent,
        CreateLocationDialogComponent,
        CategoryComponent,
        LocationComponent,
        AppComponent,
        HomeComponent,
        AboutComponent,
        // tenants
        TenantsComponent,
        CreateTenantDialogComponent,
        EditTenantDialogComponent,
        // roles
        RolesComponent,
        CreateRoleDialogComponent,
        EditRoleDialogComponent,
        // users
        UsersComponent,
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ChangePasswordComponent,
        ResetPasswordDialogComponent,
        // layout
        HeaderComponent,
        HeaderLeftNavbarComponent,
        HeaderLanguageMenuComponent,
        HeaderUserMenuComponent,
        FooterComponent,
        SidebarComponent,
        SidebarLogoComponent,
        SidebarUserPanelComponent,
        SidebarMenuComponent,
    ],
    imports: [
        InputNumberModule,
        CalendarModule,
        DropdownModule,
        PaginatorModule,
        TableModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        BsDropdownModule,
        CollapseModule,
        TabsModule,
        AppRoutingModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule,
    ],
    providers: []
})
export class AppModule {}
