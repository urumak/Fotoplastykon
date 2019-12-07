export class InfiniteScroll
{
    constructor(unitSize: number) {
        this.unitSize = unitSize;
    }

    public unitSize : number;
    public rowsLoaded : number = 0;

    public setUnitSize(unitSize: number) {
        this.unitSize = unitSize;
    }
    public setRowsLoaded(rowsLoaded: number) {
        this.rowsLoaded = rowsLoaded;
    }
    public restore(unitSize?: number, rowsLoaded?: number) {
        this.unitSize = unitSize ? unitSize : 20;
        this.rowsLoaded = rowsLoaded ? rowsLoaded : 0;
    }
}

export interface InfiniteResult<T>
{
    scroll: InfiniteScroll;
    items: T[];
}

