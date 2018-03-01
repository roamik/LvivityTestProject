import { Component, OnInit } from '@angular/core';
import { TemplatesService } from '../_services/templates.service';
import { Template } from '../_models/template';

@Component({
    selector: 'templates-page',
    templateUrl: './templates-page.component.html',
    styleUrls: ['./templates-page.component.css']
})
export class TemplatesPageComponent implements OnInit {

    public templates: Array<Template> = [];

    template: Template = new Template();

    constructor(private templatesService: TemplatesService) { }

    ngOnInit() {
        this.getMyTemplates();
    }

    getMyTemplates() {
        this.templatesService.getTemplates().subscribe(
            templates => { this.templates = templates },
            error => {}
        );
    }

    addTemplate() {

        this.templatesService.add(this.template)
            .subscribe(
            template => {
                this.template = template;
            },
            error => {
            });
    }
}
