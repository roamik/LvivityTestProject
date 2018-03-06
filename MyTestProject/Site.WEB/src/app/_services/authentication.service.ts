import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from "@angular/common/http";

const Headers = new HttpHeaders({ 'Content-Type': 'application/json', 'withCredentials': 'true' });

@Injectable()
export class AuthenticationService {
    readonly BASEURL: string;
    constructor(private http: HttpClient) {
        this.BASEURL = environment.baseApi;
    }

    public register(model) {
        return this.http.post(this.BASEURL + 'api/account/register', model, { headers: Headers })
            .map((response: any) => {
                // login successful if there's a jwt token in the response
                let user = response;
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            });
    }

    public login(model) {
        debugger;
        return this.http.post(this.BASEURL + 'api/account/login', model, { headers: Headers })

            .map((response: any) => {
                // login successful if there's a jwt token in the response
                let user = response;
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}