import { Component } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoginData } from './login.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  url: string = environment.apiBaseUrl + "/Accounts/login"
  loginData: LoginData = new LoginData()

  constructor(private http: HttpClient) { }

  onSubmit(form: NgForm) {
    this.http.post(this.url, this.loginData)
      .subscribe({
        next: res => {
          console.log(res);
        },
        error: err => console.log(err)
      });
  }

}
