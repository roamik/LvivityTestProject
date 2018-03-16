import { Component, OnInit, Input } from '@angular/core';
import { Project } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';

@Component({
    selector: 'projects-list-page',
    templateUrl: './projects-list-page.component.html',
    styleUrls: ['./projects-list-page.component.css']
})
export class ProjectsListPageComponent implements OnInit {

    @Input() projects: Array<Project> = [];

    length: number;
    currentPage: number = 0;
    projectCount: number = 10;

    constructor(private projectsService: ProjectsService) {

    }

    ngOnInit() {
        this.getProjects(this.currentPage);
    }

    getProjects(page) {
        this.currentPage = page;
        this.projectsService.getProjects(this.currentPage, this.projectCount)
            .subscribe(
            pageModel => {
                this.projects = pageModel.items;
                this.length = pageModel.totalCount;
            });
    }
}
