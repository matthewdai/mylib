import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { ProjectsComponent } from './project/projects.component';
import { ProjectComponent } from './project/project.component';
import { RatingComponent } from './rating.component';
import { TryoutComponent } from './tryout.component';
import { UserFormComponent } from './user/user-form.component';

import { ProjectService } from './project/project.service';

@NgModule({
  declarations: [
    AppComponent, ProjectsComponent, ProjectComponent, RatingComponent, TryoutComponent, UserFormComponent],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  bootstrap: [AppComponent, ProjectsComponent, ProjectComponent, RatingComponent, TryoutComponent, UserFormComponent]
})
export class AppModule { }
