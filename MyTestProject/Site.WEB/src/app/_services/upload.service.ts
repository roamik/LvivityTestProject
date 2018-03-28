import { Injectable } from '@angular/core';

import { AuthGuard } from '../_guards/auth.guard';

import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class UploadService {

    readonly BASEURL: string;

    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }


    upload(id, fileToUpload: any) {
        let input = new FormData();
        input.append("file", fileToUpload);

        return this.http.post(this.BASEURL + 'api/upload/' + id, input);
    }
}