import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from "@angular/router";
import { LoginService } from "app/services/login.service";
import { Observable } from "rxjs";

@Injectable({
    providedIn : "root"
})

export class AuthGuardService implements CanActivate {
    
    private url: string;

    constructor(private login: LoginService, private router: Router) {
    }

    private authState(): boolean {
        if (this.isLoginOrRegister()) {
            this.router.navigate(['/dashboard']);
            return false;
        }
        return true;
    }

    private notAuthState(): boolean {
        if (this.isLoginOrRegister()) {
            return true;
        }
        this.router.navigate(['/login']);
        return false;
    }
    private isLoginOrRegister(): boolean {
        if (this.url.includes('/login') || this.url.includes('/register')) {
            return true;
        }
        return false;
    }
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): any {
        this.url = state.url;
        if (this.login.isAuthenticated()) {
            return this.authState();
        }
        return this.notAuthState();
    }

}