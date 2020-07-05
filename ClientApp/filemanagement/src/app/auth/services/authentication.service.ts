import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { User, UserLoginModel } from '../user.model';
import { SERVER_BASE_URL, USER_AUTHENTICATE } from 'src/app/constants/url-path.constants';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  isUserLoggedIn: boolean = false;
  userName: string;
  baseUrl = SERVER_BASE_URL;

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }


  loginUser(userLogin: UserLoginModel): Observable<any> {
    return this.http.post<User>(
      this.baseUrl + USER_AUTHENTICATE,
      userLogin
    ).pipe(map<User, User>(user => {

      localStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user);
      return user;

    }));
  }

  logOutUser() {
    this.isUserLoggedIn = false;
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

}

