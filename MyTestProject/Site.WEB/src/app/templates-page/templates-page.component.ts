import { Component, OnInit } from '@angular/core';
import { TemplatesService } from '../_services/templates.service';
import { Template } from '../_models/template';
import { Router } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

@Component({
    selector: 'templates-page',
    templateUrl: './templates-page.component.html',
    styleUrls: ['./templates-page.component.css']
})
export class TemplatesPageComponent implements OnInit {

    public templates: Array<Template> = [];

    template: Template = new Template();

    templateId: any;

    constructor(
        private guard: AuthGuard,
        private router: Router,
        private templatesService: TemplatesService
    ) { }

    ngOnInit() {
        this.getMyTemplates();
    }

    getMyTemplates() {
        this.templatesService.getTemplates().subscribe(
            templates => {
                this.templates = templates;
            },
            error => { }
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

    deleteTemplate(template) {
        this.templatesService.deleteTemplate(template.id)
            .subscribe(
            template => {
            },
            error => {
            });
    }

}
