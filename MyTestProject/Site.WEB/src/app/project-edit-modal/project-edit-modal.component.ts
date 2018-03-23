import { Component, OnInit, TemplateRef, Output, EventEmitter, ViewChild, Input, ElementRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { Project } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { UsersService } from '../_services/users.service';
import * as _ from "lodash";
import { UserProject } from '../_models/UserProject';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: 'project-edit-modal',
    templateUrl: './project-edit-modal.component.html',
    styleUrls: ['./project-edit-modal.component.css']
})
export class ProjectEditModalComponent implements OnInit {

    @ViewChild('template')
    template: TemplateRef<any>;

    @ViewChild('fileInput')
    fileInput: ElementRef;

    @Output()
    change: EventEmitter<Project> = new EventEmitter<Project>();

    form: FormGroup;

    filterargs = { id: 'hello' };

    project: Project = new Project();

    @Input()
    set projectId(value: string) {
        this.project = new Project();
        this.project.id = value;

    }

    currentPage: number = 0;
    usersCount: number = 10;

    public userProjects: Array<UserProject> = [];

    //id: string;
    modalRef: BsModalRef;
    private sub: any;

    constructor(private modalService: BsModalService,
        private projectsService: ProjectsService,
        private usersService: UsersService,
        private fb: FormBuilder,
        private route: ActivatedRoute) { this.createForm(); }

    ngOnInit() {

    }

    createForm() {
        this.form = this.fb.group({
            imageF: null
        });
    }

    afterProjectIdSet() {
        this.getProjectInfo(this.project.id);
        this.getUsersToSelect();
    }

    updateProjectInfo() {
        this.projectsService.update(this.project)
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
            pageModel => {
                this.userProjects = _.map(pageModel.items, (user) => { return new UserProject(user.id, user, this.project.id) });
            },
            error => { }
            );
    }

    onFileChange(event) {
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];
            reader.readAsDataURL(file);
            reader.onload = () => {
                this.project.image = reader.result.split(',')[1];
            };
        }
    }

    open() {
        this.modalRef = this.modalService.show(this.template);
        this.afterProjectIdSet();
    }
}
