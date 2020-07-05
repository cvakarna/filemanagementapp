import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../auth/services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(public authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }

  logout() {
    this.authService.logOutUser();
    this.router.navigate(['/home'])
  }

}
