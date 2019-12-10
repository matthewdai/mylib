import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  items = ["Adove", "Apple", "Microsoft", "IBM", "Dell", "eBay", "Tokio Morine"];

  constructor() { }

  ngOnInit() {
  }

}
