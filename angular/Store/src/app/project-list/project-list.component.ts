import { Component, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';


import { Project } from '../shared/project';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {
  closeResult: string;
  display='nont';

  projects: Project[] = [
    {id:1, name: "Project 1", owner: "John", dateCreated:new Date("1/1/2019"), detail: false}, 
    {id:2, name: "Project 2", owner: "Matthew", dateCreated:new Date("1/1/2018"), detail: true}, 
    {id:3, name: "Project 3", owner: "Jess", dateCreated:new Date("5/1/2019"), detail: false}
  ];

  constructor(private modalService: NgbModal) {
   }

  ngOnInit() {
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }

  openModal() {
    //window.alert("About to open modal!");
    //this.display='block';
  }

  onCloseHandled() {
    this.display = 'none';
  }

}
