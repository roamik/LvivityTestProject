import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
    selector: 'app-register-page',
    templateUrl: './register-page.component.html',
    styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

    model: any = {};

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    ngOnInit() {

        this.authenticationService.logout();

    }


    register() {
        this.authenticationService.register(this.model)
            .subscribe(
            data => {
                this.router.navigate(['/home']);
            },
            error => {

            });
    }
}
