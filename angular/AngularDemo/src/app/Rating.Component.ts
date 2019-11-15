import { Component } from '@angular/core';

@Component({
    selector: 'rating',
    template: `
    <i class="glyphicon" [class.glyphicon-star-emty]="rating<1" [class.glyphicon-star]="rating>=1"> 
    </i>
    <button type="button" class="btn btn-default btn-lg">
        <span class="glyphicon glyphicon-star" aria-hidden="true"></span>Star
    </button>
    `

})

export class RatingComponent {
  rating = 0;

  onClick(ratingValue) {
      this.rating = ratingValue;
  }
}
