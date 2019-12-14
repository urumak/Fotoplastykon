export class InfiniteScroll
{
    constructor(unitSize: number) {
        this.unitSize = unitSize;
        Object.setPrototypeOf(this, new.target.prototype);
    }

    public unitSize : number;
    public rowsLoaded : number = 0;

    setUnitSize(unitSize: number) {
        this.unitSize = unitSize;
    }
    setRowsLoaded(rowsLoaded: number) {
        this.rowsLoaded = rowsLoaded;
    }

    restore(unitSize?: number, rowsLoaded?: number) {
        this.unitSize = unitSize ? unitSize : 20;
        this.rowsLoaded = rowsLoaded ? rowsLoaded : 0;
    }
}

export interface InfiniteScrollResult<T>
{
    scroll: InfiniteScroll;
    items: T[];
}
