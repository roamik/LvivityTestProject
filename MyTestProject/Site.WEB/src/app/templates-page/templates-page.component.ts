import { Component, OnInit, TemplateRef } from '@angular/core';
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

    length: number;
    currentPage: number = 0;
    templateCount: number = 4;

    public templates: Array<Template> = [];

    template: Template = new Template();

    templateId: any;
    
    constructor(
        private guard: AuthGuard,
        private router: Router,
        private templatesService: TemplatesService
    ) { }

    ngOnInit() {
        this.getMyTemplates(this.currentPage);
    }


    getMyTemplates(page) {
        this.currentPage = page;
        this.templatesService.getTemplates(this.currentPage, this.templateCount)
            .subscribe(
            pageModel => {
                this.templates = pageModel.items;
                this.length = pageModel.totalCount;
            },
            error => { }
            );
    }

    addTemplate() {
        this.templatesService.add(this.template)
            .subscribe(
            template => {
                this.template = template;
                this.getMyTemplates(this.currentPage);
            },
            error => {
            });
    }

    deleteTemplate(template) {
        this.templatesService.deleteTemplate(template.id)
            .subscribe(
            success => {

                this.getMyTemplates(this.currentPage);
            },
            error => {
            });
    }

}
