import { Component, OnInit } from '@angular/core';
import { DatasetItem } from '../../../shared/dataset-item';

@Component({
  selector: 'app-dataset-list',
  templateUrl: './dataset-list.component.html',
  styleUrls: ['./dataset-list.component.css']
})
export class DatasetListComponent implements OnInit {

  items: DatasetItem[] = [
    { name: "Dataset 1", description: "This is the first dataset" },
    { name: "Dataset 2", description: "This is the second dataset" },

  ]

  constructor() { }

  ngOnInit() {
  }

}
