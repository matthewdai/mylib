import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  projects = [
    {id:1, name: "Project 1", owner: "John", dateCreated: "1/1/2019"}, 
    {id:2, name: "Project 2", owner: "Matthew"}, 
    {id:3, name: "Project 3", owner: "Jess"}
  ];

  constructor() {
   }

  ngOnInit() {
  }

}
