import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  isAuthorized = false

  constructor(private authService: AuthService) { 
    this.isAuthorized = this.authService.hasValidToken();
  }

  ngOnInit(): void {
  }

  logOut() {
    this.authService.removeUser();
  }
}
