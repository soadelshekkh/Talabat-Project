using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.spcifications
{
    public class productSpecififcationPrams
    {
        private const int MaxPageSize = 10;
        public string sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        private string Search;

        public string search
        {
            get { return Search; }
            set { Search = value.ToLower(); }
        }

        public int PageIndex { get; set; } = 1; 
        private int PageSize = 5;

        public int pageSize
        {
            get { return PageSize; }
            set { PageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

    }
}
