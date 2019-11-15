import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
})
export class AppComponent {
  title = "TMHCC Application";
  today = new Date();
  // onClick($event) {
  //   this.title = $event;
  // }
}
