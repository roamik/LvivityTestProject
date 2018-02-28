import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';


@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    private get currentUser(): any { return localStorage.getItem('currentUser') !== null ? JSON.parse(localStorage.getItem('currentUser')) : null }

    public get userId(): string { return this.currentUser !== null ? this.currentUser.id : null }

    public get token(): string { return this.currentUser !== null ? this.currentUser.token : null }

    get isAuthenticated(): boolean { return this.token !== null }; //true if token exists in localStorage

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.isAuthenticated) {
            
        }
        else {
            // not logged in so redirect to login page with the return url
            this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        }
        return this.isAuthenticated;
    }
}