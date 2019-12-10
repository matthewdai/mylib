import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart.service';

@Component({
  selector: 'app-list-demo',
  templateUrl: './list-demo.component.html',
  styleUrls: ['./list-demo.component.css']
})
export class ListDemoComponent implements OnInit {

  items;
  
  constructor(
    private cartService: CartService
  ) { }

  
  ngOnInit() {
    this.items = this.cartService.getShippingPrices();
  }


}

