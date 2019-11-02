import { Component } from '@angular/core';
import { ProjectService } from './project.service';

@Component({
  selector: 'projects',
  templateUrl: 'projects.component.html',
  providers: [ProjectService],
})
export class ProjectsComponent {
  projects;

  constructor(ps : ProjectService) {
    this.projects = ps.getProjects();
  }

}
