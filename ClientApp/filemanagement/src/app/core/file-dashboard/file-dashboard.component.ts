import { Component, OnInit } from '@angular/core';
import { FileService } from '../services/file.service';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-file-dashboard',
  templateUrl: './file-dashboard.component.html',
  styleUrls: ['./file-dashboard.component.scss']
})
export class FileDashboardComponent implements OnInit {
  displayedColumns: string[] = ['position', 'name', 'modifiedDateTime', 'symbol'];
  dataSource: File[] = [];
  filesNotAvilable: boolean = false;

  constructor(private fileService: FileService) { }

  ngOnInit(): void {
    this.fileService.getFiles().subscribe(res => {
      console.log('File Service getall', res);
      this.processResponseData(res as Array<any>)
    });

  }

  private processResponseData(response: Array<any>) {
    if (response && response.length > 0) {
      this.dataSource = [...response]
      console.log('Data Source', this.dataSource);
      this.filesNotAvilable = false;
    } else {
      this.filesNotAvilable = true;
    }
  }

  onDownload(file: File) {
    console.log('Ondownload', event);
    this.fileService.downloadFile(file.name).subscribe(data => {
      console.log('downloaddata', data);
      this.downloadResponseFile(data, file.name)
    });
  }

  private downloadResponseFile(data, fileName) {
    const downloadedFile = data;
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = fileName;
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }

}

export interface File {
  name: string;
  position: number;
  symbol: string;
  modifiedDateTime: Date
}

