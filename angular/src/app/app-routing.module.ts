import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { LocationComponent } from './locations/locations.component';
import { CategoryComponent } from './categories/categories.component';
import { ProductComponent } from './products/products.component';
import { AuctionComponent } from './auctions/auctions.component';
import { ViewAuctionComponent } from './auctions/view-auction/view-auction/view-auction.component';
import { SoldProductComponent } from './sold-products/sold-products.component';
import { BuyProductComponent } from './buy-products/buy-products.component';
import { ReadyForSellProductComponent } from './readyForSell-products/readyForSell-products.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'readyforSell', component: ReadyForSellProductComponent, data: { permission: 'Pages.Products' }, canActivate: [AppRouteGuard] },
                    { path: 'buyProducts', component: BuyProductComponent, data: { permission: 'Pages.Products' }, canActivate: [AppRouteGuard] },
                    { path: 'soldProducts', component: SoldProductComponent, data: { permission: 'Pages.Products' }, canActivate: [AppRouteGuard] },
                    { path: 'viewAuctions', component: ViewAuctionComponent, data: { permission: 'Pages.Auctions' }, canActivate: [AppRouteGuard] },
                    { path: 'auctions', component: AuctionComponent, data: { permission: 'Pages.Auctions' }, canActivate: [AppRouteGuard] },
                    { path: 'products', component: ProductComponent, data: { permission: 'Pages.Products' }, canActivate: [AppRouteGuard] },
                    { path: 'categories', component: CategoryComponent, data: { permission: 'Pages.Categories' }, canActivate: [AppRouteGuard] },
                    { path: 'locations', component: LocationComponent, data: { permission: 'Pages.Locations' }, canActivate: [AppRouteGuard] },
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
