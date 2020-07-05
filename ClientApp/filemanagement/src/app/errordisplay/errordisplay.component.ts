import { Component, OnInit } from '@angular/core';
import { ServerexceptionproviderService } from '../serverexceptionprovider.service';

@Component({
  selector: 'errordisplay',
  templateUrl: './errordisplay.component.html',
  styleUrls: ['./errordisplay.component.scss']
})
export class ErrordisplayComponent implements OnInit {

  errorMessageToShow: any = null;
  constructor(private serverErrorService: ServerexceptionproviderService) { }

  ngOnInit(): void {
    this.serverErrorService.serverError$.subscribe(errorMessage => {
      this.errorMessageToShow = errorMessage.Message;
    });
  }

}
