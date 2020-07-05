import { Injectable } from '@angular/core';
import { AuthenticationService } from '../auth/services/authentication.service';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthgaurdService implements CanActivate {

  constructor(private authService: AuthenticationService, private router: Router) {

  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authService.currentUserValue;
    if (!currentUser) {
      this.router.navigate(['login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
    return true;
  }
}
