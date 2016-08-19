using System.Threading.Tasks;
using AngularJS2_WebAPI_Pagination.API.Models;

namespace AngularJS2_WebAPI_Pagination.API.Repositories
{
    public interface IProductRepository
    {
        Task<PagedCollection<Product>> ReadAsync(int? page, int? pageSize);
        Task<Product> ReadOne(int id);
    }
}