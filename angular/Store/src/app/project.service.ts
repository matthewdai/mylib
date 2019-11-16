import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ProjectService {
  
  projects = [
    {id:1, name: "Project 1", owner: "John", dateCreated: "1/1/2019"}, 
    {id:2, name: "Project 2", owner: "Matthew"}, 
    {id:3, name: "Project 3", owner: "Jess"},
    {id:5, name: "Project Producer", owner: "Kenan"},
  ];

  constructor() { }

  getProjects() {
    return this.projects;
  }

  getProject(projectId: number) {
    return this.projects.find(x => x.id == projectId);
  }
}
