import { Component} from '@angular/core';
import { LoginService } from './services/login.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isLogin = false;
  constructor(private loginService: LoginService) {
    this.checkIsLogin();
  }

  checkIsLogin() {
    this.isLogin = this.loginService.isAuthenticated();
  }
}
