import { Component, OnInit } from '@angular/core';
import { AuthGuard } from '../_guards/auth.guard';

@Component({
    selector: 'navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

    get isAuthenticated(): boolean { return this.guard.isAuthenticated }

    constructor(private guard: AuthGuard) { }

    private _opened: boolean = false;

    private _toggleOpened(): void {
        this._opened = !this._opened;
    }

    ngOnInit() {
    }



}
