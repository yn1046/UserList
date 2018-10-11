import { Component } from '@angular/core';
import User from './models/user';
import DataService from './services/data.service';

@Component({
    selector: 'app',
    templateUrl: 'app.html',
    providers: [DataService]
})
export class AppComponent {
    users: User[];
    pages: number[];
    pageCount: number;

    constructor(private dataService: DataService) { }

    async ngOnInit() {
        this.loadUsers(0);
        await this.loadPageCount();
        this.setPage(0);
    }

    loadUsers(page: number) {
        this.dataService.getUsers(page)
            .subscribe((data: User[]) => this.users = data);
    }

    async loadPageCount() {
        let pageCount: number;
        await this.dataService.getPageCount().toPromise()
            .then((data: number) => this.pageCount = data);
    }

    setPage(page: number) {
        let pages: number[] = [];
        for (let i = 0; i < this.pageCount; i++) pages.push(i);

        let leftBorder: number = page - 5;
        let rightBorder: number = page + 5;
        leftBorder = Math.max(leftBorder, 0);
        rightBorder = Math.min(rightBorder, this.pageCount);
        if (page < 5) rightBorder = Math.min(10, this.pageCount);
        else if (page > this.pageCount - 5) leftBorder = Math.max(0, this.pageCount - 10);

        this.pages = pages.slice(leftBorder, rightBorder);
        this.loadUsers(page);
    }
}