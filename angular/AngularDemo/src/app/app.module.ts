import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectComponent } from './project-list/project/project.component';
import { UserFormComponent } from './user/user-form.component';
import { TopBarComponent } from './top-bar/top-bar.component';

import { ProjectService } from './project-list/project-list.service';

@NgModule({
  declarations: [
    AppComponent, 
    ProjectListComponent, 
    ProjectComponent, 
    UserFormComponent, 
    TopBarComponent],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      { path: '', component: ProjectListComponent },
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
