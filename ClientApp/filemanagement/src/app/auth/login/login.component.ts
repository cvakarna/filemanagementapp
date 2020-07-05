import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { catchError, filter } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { ServerexceptionproviderService } from 'src/app/serverexceptionprovider.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  previousUrl;
  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.initLoginForm();
    this.previousUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
    console.log(this.previousUrl);

  }

  initLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl("", [Validators.email, Validators.required]),
      password: new FormControl("", Validators.required)
    });
  }

  onSubmit() {
    console.log('after submitiing login form ', this, this.loginForm.value);
    this.authService.loginUser({ ...this.loginForm.value }).subscribe((res) => {
      console.log(res);
      if (res != null) {
        this.authService.isUserLoggedIn = !this.authService.isUserLoggedIn;
        this.router.navigate([this.previousUrl]);

      }
    });
  }

}
