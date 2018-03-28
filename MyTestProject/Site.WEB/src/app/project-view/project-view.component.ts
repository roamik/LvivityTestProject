import { Component, OnInit, Input } from '@angular/core';
import { Project } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';

@Component({
    selector: 'project-view',
    templateUrl: './project-view.component.html',
    styleUrls: ['./project-view.component.css']
})
export class ProjectViewComponent implements OnInit {

    @Input() project: Project;
    constructor(private projectsService: ProjectsService) { }



    ngOnInit() {
        this.getUpdatedProjectInfo(this.project.id);
    }

    getUpdatedProjectInfo(id: string) {
        this.projectsService.getById(id)
            .subscribe(
                project => {
                    this.project = project;
                },
                error => { }
            );
    }

}
