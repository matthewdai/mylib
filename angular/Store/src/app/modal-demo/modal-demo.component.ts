import { Component, OnInit } from '@angular/core';
import { ModalService } from '../_modal';

@Component({
  selector: 'app-modal-demo',
  templateUrl: './modal-demo.component.html',
  styleUrls: ['./modal-demo.component.css']
})
export class ModalDemoComponent implements OnInit {

  closeResult: string = "close eresult here!";

  constructor(private modalService: ModalService) { }

  ngOnInit() {
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

  closeModal(id: string) {
      this.modalService.close(id);
  }

}
