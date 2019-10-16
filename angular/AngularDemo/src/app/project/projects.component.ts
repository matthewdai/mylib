import { Component } from '@angular/core';
import { ProjectService } from './project.service';

@Component({
  selector: 'projects',
  templateUrl: 'projects.component.html',
  providers: [ProjectService],
})
export class ProjectsComponent {
  projects;

  getProjects() : string[] {
        return ["Angularjj", "Pro typeScript", "ASP.NET"];
        //return ["Learning Angular 2", "Pro typeScript", "ASP.NET"];
  }

  constructor( ps : ProjectService) {
    this.projects = ps.getProjects();
  }

}
