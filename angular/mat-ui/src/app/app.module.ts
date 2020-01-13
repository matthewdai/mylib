import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MaterialModule } from './material/material.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Routes, RouterModule } from '@angular/router';
import { TopNavComponent } from './top-nav/top-nav.component';
import { FormsModule } from '@angular/forms';

import { SettingsComponent } from './settings/settings.component';
import { LayoutComponent } from './layout/layout.component';
import { TypophyComponent } from './typophy/typophy.component';
import { ButtonComponent } from './button/button.component';
import { BadgeComponent } from './badge/badge.component';
import { MenuComponent } from './menu/menu.component';
import { ListComponent } from './list/list.component';
import { GridListComponent } from './grid-list/grid-list.component';
import { ExpansionPanelComponent } from './expansion-panel/expansion-panel.component';
import { FormComponent } from './form/form.component';
import { DialogComponent } from './dialog/dialog.component';
import { Dialog2Component } from './dialog2/dialog2.component';


// import { MatNavBarComponent } from './mat-nav-bar/mat-nav-bar.component';



const routes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full"},
    { path: "home", component: DashboardComponent },
    { path: "settings", component: SettingsComponent },
    { path: "layout", component: LayoutComponent },
    { path: "typophy", component: TypophyComponent },
    { path: "buttons", component: ButtonComponent },
    { path: "badge", component: BadgeComponent },
    { path: "menu", component: MenuComponent },
    { path: "list", component: ListComponent },
    { path: "gridlist", component: GridListComponent },
    { path: "expansionpanel", component: ExpansionPanelComponent },
    { path: "form", component: FormComponent },
    { path: "dialog", component: DialogComponent },
]


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    TopNavComponent,
    SettingsComponent,
    LayoutComponent,
    TypophyComponent,
    ButtonComponent,
    BadgeComponent,
    MenuComponent,
    ListComponent,
    GridListComponent,
    ExpansionPanelComponent,
    FormComponent,
    DialogComponent,
    Dialog2Component,
  ],
  entryComponents: [Dialog2Component],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    RouterModule.forRoot(routes, { useHash: true})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
