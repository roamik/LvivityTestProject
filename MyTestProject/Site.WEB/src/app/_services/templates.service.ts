import { Injectable } from '@angular/core';

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Template } from '../_models/template';
import { PageModel } from '../_models/PageModel';
import { Transaction } from '../_models/transaction';


@Injectable()
export class TemplatesService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

    getById(id: string): Observable<Template> {
        return this.http.get<Template>(this.BASEURL + 'api/templates/' + id);
    }

    getTransactions(page: number, count: number): Observable<PageModel<Transaction>> {
        var url = 'api/transactions/paged?page=' + page + '&count=' + count;
        return this.http.get<PageModel<Transaction>>(this.BASEURL + url);
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

    formTransaction(model): Observable<Transaction> {
        return this.http.post<Transaction>(this.BASEURL + 'api/transactions/form', model);
    }

    getBalance(model): Observable<Transaction> {
        return this.http.post<Transaction>(this.BASEURL + 'api/transactions/balance', model);
    }
}