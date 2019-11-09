export default class Pager
{
    constructor(index: number, size: number)
    {
        this.pageIndex = index;
        this.pageSize = size;
    }

    public pageIndex : number;
    public pageSize : number;
    public totalRows : number = 0;
    public totalPages : number = 0;
}
