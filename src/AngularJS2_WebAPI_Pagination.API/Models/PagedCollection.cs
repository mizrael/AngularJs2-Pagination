using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularJS2_WebAPI_Pagination.API.Models
{
    public class PagedCollection<T>
    {
        public PagedCollection(IEnumerable<T> items, int page, int totalPages, int totalCount)
        {
            if (null == items)
                throw new ArgumentNullException(nameof(items));
            this.Items = items;
            this.Page = page;
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
        }

        public int Page { get; private set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }

        public IEnumerable<T> Items { get; private set; }
    }

}