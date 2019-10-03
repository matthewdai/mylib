import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template:`
    <rating></rating>
    `,
})
export class AppComponent {
  title = "hello";

  onClick($event) {
    this.title = $event;
  }
}
