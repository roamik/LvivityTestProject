import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";

import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppComponent } from './app.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AuthenticationService } from './_services/authentication.service';
import { RegisterPageComponent } from './register-page/register-page.component';
import { AuthGuard } from './_guards/auth.guard';
import { NavbarComponent } from './navbar/navbar.component';
import { TemplatesPageComponent } from './templates-page/templates-page.component';
import { TemplatesService } from './_services/templates.service';
import { TokenInterceptor } from './_interceptors/token.interceptor';
import { EditPageComponent } from './edit-page/edit-page.component';


const appRoutes: Routes = [
    { path: "login", component: LoginPageComponent },
    { path: "register", component: RegisterPageComponent },
    { path: "home", component: HomePageComponent, canActivate: [AuthGuard] },
    { path: "templates", component: TemplatesPageComponent, canActivate: [AuthGuard] },
    { path: "edit/:id", component: EditPageComponent, canActivate: [AuthGuard] },

    {
        path: "",
        redirectTo: "/home",
        pathMatch: "full"
    }
];

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
}


@NgModule({
    declarations: [
        AppComponent,
        LoginPageComponent,
        HomePageComponent,
        RegisterPageComponent,
        NavbarComponent,
        TemplatesPageComponent,
        EditPageComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(appRoutes),
        FormsModule,
        HttpClientModule
    ],
    providers: [{
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        },
        AuthenticationService,
        TemplatesService,
        AuthGuard
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
