import { Component, OnInit } from '@angular/core';
import { ProjectsService } from '../_services/projects.service';
import { Router } from '@angular/router';
import { Project } from '../_models/project';

@Component({
    selector: 'project-creation-page',
    templateUrl: './project-creation-page.component.html',
    styleUrls: ['./project-creation-page.component.css']
})
export class ProjectCreationPageComponent implements OnInit {

    project: Project = new Project();

    constructor(
        private projectsService: ProjectsService,
        private router: Router
    ) { }

    ngOnInit() {
    }

    addProject() {
        
        this.projectsService.add(this.project)
            .subscribe(
            project => {
                this.project = project;
                this.router.navigate(['/projects']);
            },
            error => {
            });
    }
}
