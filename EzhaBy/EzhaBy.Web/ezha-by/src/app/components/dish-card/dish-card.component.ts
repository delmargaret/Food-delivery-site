import { Component, Input, OnInit } from '@angular/core';
import { Dish } from 'src/app/models/dish';

@Component({
  selector: 'app-dish-card',
  templateUrl: './dish-card.component.html',
  styleUrls: ['./dish-card.component.scss'],
})
export class DishCardComponent implements OnInit {
  @Input() dish: Dish | null = null;

  constructor() {}

  ngOnInit(): void {}

  get mainPicture() {
    return this.dish?.dishIconUrl
      ? this.dish?.dishIconUrl
      : '../../../assets/19.png';
  }
}
