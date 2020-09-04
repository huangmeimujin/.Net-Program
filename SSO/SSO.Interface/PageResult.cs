using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Interface
{
    public class PageResult<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public List<T> Results{ get; set; }
    }
}
