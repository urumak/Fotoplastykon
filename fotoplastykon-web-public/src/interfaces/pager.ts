export class Pager
{
    constructor(index: number, size: number) {
        this.pageIndex = index;
        this.pageSize = size;
    }

    public pageIndex : number;
    public pageSize : number;
    public totalRows : number = 1;
    public search: string = '';

    public get totalPages(): number {
        if(this.totalRows === 0) return 1;
        return Math.ceil(this.totalRows / this.pageSize);
    }

    public setTotalRows(rowsCount: number) {
        this.totalRows = rowsCount;
    }
    public setPageIndex(index: number) {
        this.pageIndex = index;
    }
    public setPageSize(size: number) {
        this.pageSize = size;
    }
}

export interface PaginationResult<T>
{
    pager: Pager;
    items: T[];
}

