import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cafe-page',
  templateUrl: './cafe-page.component.html',
  styleUrls: ['./cafe-page.component.css']
})
export class CafePageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  getActive(event: Event) {
    event.preventDefault();
  }

  getClosed(event: Event) {
    event.preventDefault();
  }

  getAll(event: Event) {
    event.preventDefault();
  }
}
