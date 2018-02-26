import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";

import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppComponent } from './app.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AuthenticationService } from './_services/authentication.service';


const appRoutes: Routes = [
  { path: "login", component: LoginPageComponent },
  { path: "home", component: HomePageComponent },

  {
    path: "",
    redirectTo: "/login",
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
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    HttpClientModule
  ],
  providers: [
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
