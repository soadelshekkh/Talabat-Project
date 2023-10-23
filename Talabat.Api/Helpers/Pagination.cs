using System.Collections.Generic;

namespace Talabat.Api.Helpers
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int pageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
        public Pagination(int size, int index,int count, IEnumerable<T> data)
        {
            this.PageSize = size;
            this.pageIndex = index;
            this.Data = data;
            this.Count = count;
        }
    }
}
