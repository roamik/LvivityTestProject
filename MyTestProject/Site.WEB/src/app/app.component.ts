import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'app';

    private _opened: boolean = true;

    private _toggleOpened(): void {
        this._opened = !this._opened;
    }
}
