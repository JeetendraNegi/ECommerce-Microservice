import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { LoginModel } from "app/models/login/login.model";
import { environment } from "environments/environment";
import * as moment from "moment";
import { Observable } from "rxjs";

@Injectable({
    providedIn : 'root'
})

export class LoginService {

    apiUrl = environment.APIUrl+'l/login'
    decodedToken = localStorage.getItem('expin');

    constructor(private client: HttpClient){  }

    userLogin(userlogin: LoginModel) : Observable<any> {
        return this.client.post<any>(this.apiUrl, userlogin);
    }

    isAuthenticated() : boolean {
        // return moment().isBefore(moment.unix(this.decodedToken));
        return moment().isBefore(this.decodedToken);
    }
}