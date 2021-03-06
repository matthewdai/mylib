import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductAlertsComponent } from './product-alerts/product-alerts.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { CartComponent } from './cart/cart.component';
import { ShippingComponent } from './shipping/shipping.component';
import { TestComponent } from './test/test.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { NavbarComponent } from './navbar/navbar.component';
import { StartComponent } from './start/start.component';
import { ProjectComponent } from './project-list/project/project.component';

import { CartService } from './cart.service';
import { ProjectService } from './project.service';
import { DatasetListComponent } from './project-list/project/dataset-list/dataset-list.component';
import { ProjectEditComponent } from './project-list/project-edit/project-edit.component';
import { NavDemoComponent } from './nav-demo/nav-demo.component';
import { SelectStyleComponent } from './select-style/select-style.component';
import { ListDemoComponent } from './list-demo/list-demo.component';
import { TableDemoComponent } from './table-demo/table-demo.component';
import { CarouselComponent } from './carousel/carousel.component';
import { CardsComponent } from './cards/cards.component';
import { QuickShopComponent } from './quick-shop/quick-shop.component';
import { ModalDemoComponent } from './modal-demo/modal-demo.component';
import { FlexboxDemoComponent } from './flexbox-demo/flexbox-demo.component';
import { FormDemoComponent } from './form-demo/form-demo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule } from '@angular/material';
import { MatNavBarComponent } from './mat-nav-bar/mat-nav-bar.component';


@NgModule({
  imports: [
    BrowserModule, BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule,
    RouterModule.forRoot([
      { path: '', component: StartComponent },
      { path: 'products/:productId', component: ProductDetailsComponent },
      { path: 'cart', component: CartComponent },
      { path: 'shipping', component: ShippingComponent },
      { path: 'testing', component: TestComponent },
      { path: 'projects', component: ProjectListComponent },
      { path: 'project/:projectId', component: ProjectComponent },
      { path: 'navdemo', component: NavDemoComponent },
      { path: 'tabledemo', component: TableDemoComponent },
      { path: 'selectstyle', component: SelectStyleComponent },

      { path: 'carousel', component: CarouselComponent },
      { path: 'cards', component: CardsComponent },
      { path: 'quickshop', component: QuickShopComponent },
      { path: 'listdemo', component: ListDemoComponent },
      { path: 'modaldemo', component: ModalDemoComponent },
      { path: 'flexdemo', component: FlexboxDemoComponent },
      { path: 'formdemo', component: FormDemoComponent },
    ])
  ],
  declarations: [
    AppComponent,
    TopBarComponent,
    ProductListComponent,
    ProductAlertsComponent,
    ProductDetailsComponent,
    CartComponent,
    ShippingComponent,
    TestComponent,
    ProjectListComponent,
    NavbarComponent,
    StartComponent,
    ProjectComponent,
    DatasetListComponent,
    ProjectEditComponent,
    NavDemoComponent,
    SelectStyleComponent,
    ListDemoComponent,
    TableDemoComponent,
    CarouselComponent,
    CardsComponent,
    QuickShopComponent,
    ModalDemoComponent,
    FlexboxDemoComponent,
    FormDemoComponent,
    MatNavBarComponent,
  ],
  bootstrap: [ AppComponent ],
  providers: [CartService, ProjectService]
})
export class AppModule { }

