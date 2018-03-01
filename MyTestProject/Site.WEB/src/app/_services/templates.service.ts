import { Injectable } from '@angular/core';
import { Http} from '@angular/http';

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Template } from '../_models/template';

@Injectable()
export class TemplatesService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

    getTemplates(): Observable<Template[]> {
        var url = 'api/templates/my' ;
        return this.http.get<Template[]>(this.BASEURL + url);
    }

    add(model): Observable<Template> {
        return this.http.post<Template>(this.BASEURL + 'api/templates', model);
    }
}