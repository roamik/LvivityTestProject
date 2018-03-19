import { Component, OnInit, TemplateRef, Output, EventEmitter, ViewChild } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { Project } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { UsersService } from '../_services/users.service';

@Component({
    selector: 'project-edit-modal',
    templateUrl: './project-edit-modal.component.html',
    styleUrls: ['./project-edit-modal.component.css']
})
export class ProjectEditModalComponent implements OnInit {

    @ViewChild('template')
    template: TemplateRef<any>;

    @Output()
    change: EventEmitter<Project> = new EventEmitter<Project>();

    currentPage: number = 0;
    usersCount: number = 10;

    public users: Array<User> = [];

    project: Project = new Project();

    id: string;
    modalRef: BsModalRef;
    private sub: any;

    constructor(private modalService: BsModalService,
        private projectsService: ProjectsService,
        private usersService: UsersService,
        private route: ActivatedRoute) { }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; // (+) converts string 'id' to a number
            this.getProjectInfo(this.id);
        });
        

        this.getUsersToSelect();
    }

    updateProjectInfo() {
        debugger;
        this.projectsService.update(this.project, this.id)
            .subscribe(
            project => {
                this.project = project;
                this.modalRef.hide();
                this.change.emit(this.project);
            },
            error => {
            });
    }

    getProjectInfo(id: string) {
        this.projectsService.getById(id).subscribe(
            project => { this.project = project },
            error => { }
        );
    }

    getUsersToSelect() {
        this.usersService.getUsers(this.currentPage, this.usersCount)
            .subscribe(
            pageModel => { this.users = pageModel.items },
            error => { }
            );
    }

    open() {
        this.modalRef = this.modalService.show(this.template);
    }
}
