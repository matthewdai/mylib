import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ChartErrorEvent, ChartEvent, GoogleChartComponent } from 'angular-google-charts';

@Component({
  selector: 'app-google-combo-chart',
  templateUrl: './google-combo-chart.component.html',
  styleUrls: ['./google-combo-chart.component.css']
})
export class GoogleComboChartComponent implements OnInit {  
  
  constructor() { }  
  
  title = 'Company Hiring Report';  
  type = 'ComboChart';  
  data = [  
     ["Account", 3, 2, 2.5],  
     ["HR",2, 3, 2.5],  
     ["IT", 1, 5, 3],  
     ["Sales", 3, 9, 6],  
     ["Marketing", 4, 2, 3]  
  ];  
  columnNames = ['Loaction','India','US','Average'];  
  options = {     
     hAxis: {  
        title: 'Department'  
     },  
     vAxis:{  
        title: 'Employee hired'  
     },  
     seriesType: 'bars',  
     series: {2: {type: 'line'}}  
  };  
  width = 600;  
  height = 400;  
  
  ngOnInit() {  
  }  
  
}  