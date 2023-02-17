import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from 'app/models/login/login.model';
import { LoginService } from 'app/services/login.service';

@Component({
  selector: 'login-user',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginUserComponent implements OnInit {

  user : LoginModel = new LoginModel;


  constructor(private loginService : LoginService, private router: Router) { }

  ngOnInit(): void {
  }

  loginUserSubmit() {    
    this.loginService.userLogin(this.user).subscribe(response => {
        localStorage.setItem('accessToken', response.token );
        localStorage.setItem('expin', response.expiration );
        localStorage.setItem('user', response.user );
      });    
    this.router.navigate(['/address']);
  }
}