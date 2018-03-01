import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { Router } from '@angular/router';


@Component({
    selector: 'login-page',
    templateUrl: './login-page.component.html',
    styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

    error: any = {};
    model: any = {};

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    ngOnInit() {
        this.authenticationService.logout();
    }


    login() {
        this.authenticationService.login(this.model)
            .subscribe(
            result => {
                this.router.navigate(['/home']);
            },
            response => {
                this.error = response.error;
            });
    }
}
