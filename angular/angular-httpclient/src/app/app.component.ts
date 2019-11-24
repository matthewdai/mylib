import { Component } from '@angular/core';
import { ApiService } from './api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'angular-httpclient';
  smartphone: any = [];

  constructor(private api: ApiService) {
    this.getSmartphones();
  }

  getSmartphones() {
    this.api.getSmartphone()
      .subscribe(data => {
        for (const d of (data as any)) {
          this.smartphone.push({
            name: d.name,
            price: d.price
          });
        }
        console.log(this.smartphone);
      });
  }
}
