import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { SERVER_BASE_URL, FILE_UPLOAD, FILES, FILE_DOWNLOAD } from '../../constants/url-path.constants';
import { AuthenticationService } from 'src/app/auth/services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private SERVER_URL: string = SERVER_BASE_URL;
  private FILE_UPLOADPATH: string = FILE_UPLOAD;
  private FILES: string = FILES;
  private DOWNLOADFILE: string = FILE_DOWNLOAD;
  constructor(private httpClient: HttpClient, private authService: AuthenticationService) { }

  uploadFile(formData: FormData): Observable<any> {
    let user = this.authService.currentUserValue;
    return this.httpClient.post(this.SERVER_URL + this.FILE_UPLOADPATH + '?userId=' + user.userId, formData, { reportProgress: true, observe: 'events' });
  }

  getFiles() {
    let user = this.authService.currentUserValue;
    return this.httpClient.get(this.SERVER_URL + FILES + '?userId=' + user.userId);
  }
  downloadFile(fileName: string) {
    let user = this.authService.currentUserValue;
    let downloadInfo = { UserId: user.userId, fileName: fileName };
    return this.httpClient.post(this.SERVER_URL + this.DOWNLOADFILE, downloadInfo, {
      responseType: 'blob'
    });
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
