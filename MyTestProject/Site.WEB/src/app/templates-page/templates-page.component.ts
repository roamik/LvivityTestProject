import { Component, OnInit, TemplateRef } from '@angular/core';
import { TemplatesService } from '../_services/templates.service';
import { Template } from '../_models/template';
import { Router } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';
import { Transaction } from '../_models/transaction';


@Component({
    selector: 'templates-page',
    templateUrl: './templates-page.component.html',
    styleUrls: ['./templates-page.component.css']
})
export class TemplatesPageComponent implements OnInit {

    length: number;
    currentPage: number = 0;
    transactionCount: number = 4;

    public transactions: Array<Transaction> = [];

    transaction: Transaction = new Transaction();

    transactionId: any;

    walletBalance: any;
    
    constructor(
        private guard: AuthGuard,
        private router: Router,
        private templatesService: TemplatesService
    ) { }

    ngOnInit() {
        this.getMyTransactions(this.currentPage);
    }


    getMyTransactions(page) {
        this.currentPage = page;
        this.templatesService.getTransactions(this.currentPage, this.transactionCount)
            .subscribe(
            pageModel => {
                this.transactions = pageModel.items;
                this.length = pageModel.totalCount;
            },
            error => { }
            );
    }

    //addTemplate() {
    //    this.templatesService.add(this.template)
    //        .subscribe(
    //        template => {
    //            this.template = template;
    //            this.getMyTemplates(this.currentPage);
    //        },
    //        error => {
    //        });
    //}

    initTransaction() {
        this.templatesService.formTransaction(this.transaction)
            .subscribe(
            transaction => {
                this.transaction = transaction;
                this.getMyTransactions(this.currentPage);
            },
            error => {
            });
    }

    //getBalance() {
    //    this.templatesService.getBalance(this.contractRes)
    //        .subscribe(
    //        balance => {
    //            this.walletBalance = balance.result;
    //        },
    //        error => { }
    //        );
    //}

    //deleteTemplate(template) {
    //    this.templatesService.deleteTemplate(template.id)
    //        .subscribe(
    //        success => {

    //            this.getMyTemplates(this.currentPage);
    //        },
    //        error => {
    //        });
    //}

}
