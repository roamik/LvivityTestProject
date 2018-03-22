import { Component, OnInit, TemplateRef } from '@angular/core';
import { Project } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { UserProject } from '../_models/UserProject';

@Component({
    selector: 'project-page',
    templateUrl: './project-page.component.html',
    styleUrls: ['./project-page.component.css']
})
export class ProjectPageComponent implements OnInit {

    id: string;
    private sub: any;
    project: Project = new Project();
    public involvedUsers: Array<UserProject> = [];



    constructor(
        private projectsService: ProjectsService,
        private route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => { // takes id param from route link
            this.id = params['id']; // (+) converts string 'id' to a number

            this.getProjectInfo(this.id);
        });
    }

    onProjectInfoUpdated() {
        this.getProjectInfo(this.id);
    }

    getProjectInfo(id: string) {
        this.projectsService.getById(id)
            .subscribe(
            project => {
                this.project = project;
                this.involvedUsers = project.linkedUsers;
            },
            error => { }
            );
    }

    detachUser(model) {
        this.projectsService.detachUser(model)
            .subscribe(
                success => {
                    this.getProjectInfo(this.id);
                },
                error => {
                });
    }
}
