import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgModule } from '@angular/core';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FlexLayoutModule } from '@angular/flex-layout';
import { NavbarComponent } from './navbar/navbar.component';

//material module
import { MaterialShareModule } from './material-share/material-share.module';
import { FileUploadComponent } from './core/file-upload/file-upload.component'
import { FileService } from './core/services/file.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './auth/login/login.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { FileDashboardComponent } from './core/file-dashboard/file-dashboard.component';
import { AuthenticationService } from './auth/services/authentication.service';
import { AuthgaurdService } from './authgaurd/authgaurd.service';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';

//error service
import { ServerexceptionproviderService } from './serverexceptionprovider.service'
//interceptor
import { JwtinterceptorService } from './interceptors/jwtinterceptor.service';
import { InterceptorErrorService } from './interceptor-error.service';
import { ErrordisplayComponent } from './errordisplay/errordisplay.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FileUploadComponent,
    LoginComponent,
    LogoutComponent,
    FileDashboardComponent,
    HomeComponent,
    ErrordisplayComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    MaterialShareModule,
    HttpClientModule

  ],
  providers: [FileService,
    AuthenticationService,
    AuthgaurdService,
    ServerexceptionproviderService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtinterceptorService, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorErrorService,
      multi: true

    }],

  bootstrap: [AppComponent]
})
export class AppModule { }
