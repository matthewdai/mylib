import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from 'src/app/project.service';

import { NavItem } from '../../shared/nav-item';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  project;
  currentTab: string;

  constructor(
    private route: ActivatedRoute,
    private ps: ProjectService
  )
  {
    this.currentTab = "datasets";
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.project = this.ps.getProject(+params.get('projectId')); //products[+params.get('productId')];
    });
  }

}
