import { Component, OnInit, Input } from '@angular/core';
import { Project } from '../_models/project';

@Component({
    selector: 'project-view',
    templateUrl: './project-view.component.html',
    styleUrls: ['./project-view.component.css']
})
export class ProjectViewComponent implements OnInit {

    @Input() project: Project;
    constructor() { }

    ngOnInit() {
        
    }

}
