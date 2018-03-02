import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TemplatesService } from '../_services/templates.service';
import { Template } from '../_models/template';

@Component({
    selector: 'edit-page',
    templateUrl: './edit-page.component.html',
    styleUrls: ['./edit-page.component.css']
})
export class EditPageComponent implements OnInit {

    id: string;
    private sub: any;

    template: Template = new Template();

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private templatesService: TemplatesService
    ) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; // (+) converts string 'id' to a number
            this.getTemplateInfo(this.id);
        });
    }

    getTemplateInfo(id: string) {
        this.templatesService.getById(id).subscribe(
            template => { this.template = template },
            error => { }
        );
    }

    updateTemplateInfo() {
        this.templatesService.update(this.template, this.id)
            .subscribe(
            success => {
                this.router.navigate(['/templates']);
            },
            error => {
            });
    }

}
