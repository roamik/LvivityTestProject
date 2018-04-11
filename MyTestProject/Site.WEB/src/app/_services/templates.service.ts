import { Injectable } from '@angular/core';

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Template } from '../_models/template';
import { PageModel } from '../_models/PageModel';
import { ContractResult } from '../_models/contract.result';


@Injectable()
export class TemplatesService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

    getById(id: string): Observable<Template> {
        return this.http.get<Template>(this.BASEURL + 'api/templates/' + id);
    }

    getTemplates(page: number, count: number): Observable<PageModel<Template>> {
        var url = 'api/templates?page=' + page + '&count=' + count;
        return this.http.get<PageModel<Template>>(this.BASEURL + url);    
    }

    add(model): Observable<Template> {
        return this.http.post<Template>(this.BASEURL + 'api/templates', model);
    }

    deleteTemplate(id): Observable<Template> {
        return this.http.delete<Template>(this.BASEURL + 'api/templates/' + id);
    }

    update(model, id: string): Observable<Template> {
        return this.http.put<Template>(this.BASEURL + 'api/templates/' + id, model);
    }

    checkContract(model): Observable<ContractResult> {
        return this.http.post<ContractResult>(this.BASEURL + 'api/templates/check', model);
    }

    getBalance(model): Observable<ContractResult> {
        return this.http.post<ContractResult>(this.BASEURL + 'api/templates/balance', model);
    }
}