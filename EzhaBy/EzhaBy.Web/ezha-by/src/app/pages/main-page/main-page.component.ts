import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Towns } from 'src/app/models/towns';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent implements OnInit {
  towns = Towns;

  constructor(private router: Router) {}

  redirectToPartner() {
    this.router.navigate(['./partner']);
  }

  redirectToCourier() {
    this.router.navigate(['./courier']);
  }

  ngOnInit(): void {}
}
