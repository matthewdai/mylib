import { Component, OnInit } from '@angular/core';
import { NodeCompatibleEventEmitter } from 'rxjs/internal/observable/fromEvent';

import { NavItem } from '../shared/nav-item';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {
  // app name
  appName: string = "Peachtree";
  
  // navigation items
  navItems: NavItem[] = [
    {href: '#', label: 'Home', active: true},
    {href: '/projects', label: 'Project', active: false},
    {href: '#', label: 'Settings', active: false},
    {href: '/testing', label: 'Testing', active: true},
    {href: '#', label: 'Sign out', active: false}
];

  constructor() {
   }

  ngOnInit() {
  }

}
