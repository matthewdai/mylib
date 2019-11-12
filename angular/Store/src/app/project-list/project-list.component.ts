import { Component, OnInit } from '@angular/core';
import { Project } from '../shared/project';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  projects: Project[] = [
    {id:1, name: "Project 1", owner: "John", dateCreated:new Date("1/1/2019"), detail: false}, 
    {id:2, name: "Project 2", owner: "Matthew", dateCreated:new Date("1/1/2018"), detail: true}, 
    {id:3, name: "Project 3", owner: "Jess", dateCreated:new Date("5/1/2019"), detail: false}
  ];

  constructor() {
   }

  ngOnInit() {
  }

}
