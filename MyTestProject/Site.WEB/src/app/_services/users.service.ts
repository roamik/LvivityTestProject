import { Injectable } from '@angular/core';

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { PageModel } from '../_models/PageModel';
import { User } from '../_models/user';


@Injectable()
export class UsersService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

    getUsers(page: number, count: number): Observable<PageModel<User>> {
        var url = 'api/users?page=' + page + '&count=' + count;
        return this.http.get<PageModel<User>>(this.BASEURL + url);
    }
}