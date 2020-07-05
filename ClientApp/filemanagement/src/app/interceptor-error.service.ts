import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ServerexceptionproviderService } from './serverexceptionprovider.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorErrorService implements HttpInterceptor {

  constructor(private serverExProvider: ServerexceptionproviderService) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(this.handleError)
    )
  }
  handleError(error: HttpErrorResponse) {
    console.error(error);
    if (error.status == 400) {
      console.error('bad request' + error.error);
      this.serverExProvider.serverError$.next({ Message: error.error ? error.error.message : error.message, Type: 'badrequest' })
    }

    return throwError(error);
  }
}

export interface ErrorMessage {
  Message: string;
  Type: string
}
