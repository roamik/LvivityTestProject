import { Injectable } from '@angular/core';

import { Project } from "../_models/project";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { PageModel } from '../_models/PageModel';

@Injectable()
export class ProjectsService {

    readonly BASEURL: string;

    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

    getById(id: string): Observable<Project> {
        return this.http.get<Project>(this.BASEURL + 'api/projects/' + id);
    }

    getProjects(page: number, count: number): Observable<PageModel<Project>> {
        var url = 'api/projects?page=' + page + '&count=' + count;
        return this.http.get<PageModel<Project>>(this.BASEURL + url);
    }

    add(model): Observable<Project> {
        return this.http.post<Project>(this.BASEURL + 'api/projects', model);
    }

    update(model, id: string): Observable<Project> {
        return this.http.put<Project>(this.BASEURL + 'api/projects/' + id, model);
    }

}