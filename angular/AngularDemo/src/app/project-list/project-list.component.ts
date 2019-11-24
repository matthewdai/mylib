import { Component } from '@angular/core';
import { ProjectService } from './project-list.service';
import { ProjectComponent } from './project/project.component';

@Component({
  selector: 'project-list',
  templateUrl: 'project-list.component.html',
  styleUrls: ['project-list.component.css'],
  providers: [ProjectService],
})

export class ProjectListComponent {
  projects;

  constructor(ps : ProjectService) {
    this.projects = ps.getProjects();
  }

}
