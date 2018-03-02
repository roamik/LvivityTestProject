import { Injectable} from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthGuard } from "../_guards/auth.guard";



@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private authGuard: AuthGuard) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
            setHeaders: {
                'Authorization': `Bearer ${this.authGuard.token}`
            }
        });
        return next.handle(request);
    }
}