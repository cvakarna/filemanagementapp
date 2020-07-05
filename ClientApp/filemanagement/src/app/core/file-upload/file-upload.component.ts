import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FileService } from '../services/file.service';
import { map, catchError } from 'rxjs/operators';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { of } from 'rxjs';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {

  @ViewChild('fileUpload', { static: false }) fileUpload: ElementRef; //to query the dom elements
  files = [];
  constructor(private fileService: FileService) { }

  ngOnInit(): void {
  }

  onClick() {
    this.files = [];
    let fileUpload = this.fileUpload.nativeElement;
    fileUpload.onchange = () => {
      for (let i = 0; i < fileUpload.files.length; i++) {
        const file = fileUpload.files[i];
        this.files.push({ data: file, inProgress: false, progress: 0 });
      }
      console.log(this.files);

      this.uploadFiles();
    };
    fileUpload.click();
  }

  private uploadFiles() {
    this.fileUpload.nativeElement.value = '';
    let files = [...this.files];
    files.forEach(file => {
      this.uploadFile(file);
    });
  }

  private uploadFile(file) {
    const formData = new FormData();
    formData.append('file', file.data);
    file.inProgress = true;
    this.fileService.uploadFile(formData).pipe(
      map(event => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            file.progress = Math.round(event.loaded * 100 / event.total);
            break;
          case HttpEventType.Response:
            return event;
        }
      }),
      catchError((error: HttpErrorResponse) => {
        file.inProgress = false;
        return of(`${file.data.name} upload failed.`);
      })).subscribe((event: any) => {
        if (typeof (event) === 'object') {
          console.log(event.body);
        }
      });
  }

}
