import { Component } from '@angular/core';
import { ProductService } from './product.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
})
export class AppComponent {
  title = "Project Name";

  // onClick($event) {
  //   this.title = $event;
  // }
}
