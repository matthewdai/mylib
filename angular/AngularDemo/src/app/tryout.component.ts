import { Component } from '@angular/core';

@Component({
  selector: 'try-out',
  templateUrl: 'tryout.component.html',
})
export class TryoutComponent {
  title = "hello";

  onClick($event) {
    this.title = $event;
  }
}