import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { ProjectsComponent } from './projects.component';
import { ProjectComponent } from './project.component';
import { RatingComponent } from './rating.component';
import { TryoutComponent } from './tryout.component';

import { ProjectService } from './project.service';

@NgModule({
  declarations: [
    AppComponent, ProjectsComponent, ProjectComponent, RatingComponent, TryoutComponent],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  bootstrap: [AppComponent, ProjectsComponent, ProjectComponent, RatingComponent, TryoutComponent]
})
export class AppModule { }
