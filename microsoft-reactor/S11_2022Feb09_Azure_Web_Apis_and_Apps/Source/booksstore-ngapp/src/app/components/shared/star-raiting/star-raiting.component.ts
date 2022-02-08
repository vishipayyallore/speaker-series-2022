import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'sv-star-raiting',
  templateUrl: './star-raiting.component.html',
  styleUrls: ['./star-raiting.component.scss']
})
export class StarRaitingComponent implements OnInit {

  @Input()
  numberOfStars = 5;

  @Input()
  currentRating = 0;

  @Output()
  ratingClicked: EventEmitter<number> = new EventEmitter<number>();

  fullBandWidth = 30 * this.numberOfStars;
  ratingBandWidth = this.fullBandWidth;

  stars!: any[];
  fullBandWidthStyle!: string;

  constructor() { }

  ngOnInit(): void {
    this.stars = Array(this.numberOfStars).fill(0);
    // this.fullBandWidthStyle = `width: ${this.fullBandWidth}px;`;
  }

  ngOnChanges(): void {
    this.fullBandWidth = 30 * this.numberOfStars;
    this.fullBandWidthStyle = `width: ${this.fullBandWidth}px;`;

    this.ratingBandWidth = this.currentRating * (this.fullBandWidth / this.numberOfStars);
  }

  whenRatingClicked(): void {
    this.ratingClicked.emit(this.currentRating);
  }

}
