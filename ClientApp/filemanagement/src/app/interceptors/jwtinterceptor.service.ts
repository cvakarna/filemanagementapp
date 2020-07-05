import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../auth/services/authentication.service';

@Injectable()
export class JwtinterceptorService implements HttpInterceptor {

  constructor(private authService: AuthenticationService) {

  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //get user token and add to header before sending to servers
    let currentUser = this.authService.currentUserValue;
    if (currentUser && currentUser.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      })
    }
    return next.handle(request);
  }
}
