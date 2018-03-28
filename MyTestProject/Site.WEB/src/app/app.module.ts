import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from "@angular/common/http";
import { MatPaginatorModule } from '@angular/material/paginator';
import { SidebarModule } from 'ng-sidebar';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { ModalModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

import { AppComponent } from './app.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { NavbarComponent } from './navbar/navbar.component';
import { TemplatesPageComponent } from './templates-page/templates-page.component';
import { EditPageComponent } from './edit-page/edit-page.component';
import { ProjectsListPageComponent } from './projects-list-page/projects-list-page.component';
import { ProjectViewComponent } from './project-view/project-view.component';
import { ProjectPageComponent } from './project-page/project-page.component';
import { ProjectCreationPageComponent } from './project-creation-page/project-creation-page.component';
import { ProjectEditModalComponent } from './project-edit-modal/project-edit-modal.component';

import { ProjectsService } from './_services/projects.service';
import { AuthenticationService } from './_services/authentication.service';
import { TemplatesService } from './_services/templates.service';
import { BsModalService } from 'ngx-bootstrap';
import { UsersService } from './_services/users.service';

import { TokenInterceptor } from './_interceptors/token.interceptor';

import { AuthGuard } from './_guards/auth.guard';
import { UploadService } from './_services/upload.service';




const appRoutes: Routes = [
    { path: "login", component: LoginPageComponent },
    { path: "register", component: RegisterPageComponent },
    { path: "home", component: HomePageComponent, canActivate: [AuthGuard] },
    { path: "templates", component: TemplatesPageComponent, canActivate: [AuthGuard] },
    { path: "edit/:id", component: EditPageComponent, canActivate: [AuthGuard] },
    { path: "project/:id", component: ProjectPageComponent, canActivate: [AuthGuard] },
    { path: "projects", component: ProjectsListPageComponent, canActivate: [AuthGuard] },
    { path: "projects/create", component: ProjectCreationPageComponent, canActivate: [AuthGuard] },

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
        EditPageComponent,
        ProjectsListPageComponent,
        ProjectViewComponent,
        ProjectPageComponent,
        ProjectCreationPageComponent,
        ProjectEditModalComponent

    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(appRoutes),
        FormsModule,
        HttpClientModule,
        MatPaginatorModule,
        BrowserAnimationsModule,
        AngularFontAwesomeModule,
        NgSelectModule,
        ModalModule.forRoot(),
        SidebarModule.forRoot(),
        ReactiveFormsModule
    ],
    providers: [{
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        },
        AuthenticationService,
        TemplatesService,
        ProjectsService,
        BsModalService,
        UsersService,
        UploadService,
        AuthGuard
    ],
    bootstrap: [AppComponent]
})
export class AppModule {

}
