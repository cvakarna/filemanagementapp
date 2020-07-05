import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ErrorMessage } from './interceptor-error.service';

@Injectable({
  providedIn: 'root'
})
export class ServerexceptionproviderService {

  serverError$: Subject<ErrorMessage> = new Subject<ErrorMessage>();
  constructor() { }
}
