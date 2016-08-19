using AngularJS2_WebAPI_Pagination.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularJS2_WebAPI_Pagination.API.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        private static IEnumerable<Product> _products;

        static FakeProductRepository()
        {
            _products = _products ?? Enumerable.Range(1, 100)
                                .Select(i => new Product()
                                {
                                    Id = i,
                                    Name = "Product " + i.ToString()
                                }).ToArray();
        }

        public Task<PagedCollection<Product>> ReadAsync(int? page, int? pageSize)
        {
            var currPage = page.GetValueOrDefault(0);
            var currPageSize = pageSize.GetValueOrDefault(10);

            var paged = _products.Skip(currPage * currPageSize)
                                .Take(currPageSize)
                                .ToArray();

            var totalCount = _products.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / currPageSize);

            var coll = new PagedCollection<Product>(paged, currPage, totalPages, totalCount);
            return Task.FromResult(coll);
        }

        public Task<Product> ReadOne(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }
    }
}
