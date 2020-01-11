import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Routes, RouterModule } from '@angular/router';
import { TopNavComponent } from './top-nav/top-nav.component';
import { MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule } from '@angular/material';
import { SettingsComponent } from './settings/settings.component';
// import { MatNavBarComponent } from './mat-nav-bar/mat-nav-bar.component';



const routes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full"},
    { path: "home", component: DashboardComponent },
    { path: "settings", component: SettingsComponent },
]



@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    TopNavComponent,
    SettingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule,
    RouterModule.forRoot(routes, { useHash: true})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
