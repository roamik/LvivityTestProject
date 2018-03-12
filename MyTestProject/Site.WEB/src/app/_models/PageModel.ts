export class PageModel<T> {
    currentPage: number;
    items: T[];
    totalCount: number;

    constructor(currentPage?: number, items?: T[], totalCount?: number) {
        this.currentPage = currentPage;
        this.items = items;
        this.totalCount = totalCount;
    }
}