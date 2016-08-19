using AngularJS2_WebAPI_Pagination.API.Models;
using AngularJS2_WebAPI_Pagination.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AngularJS2_WebAPI_Pagination.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productsRepo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productsRepo, ILogger<ProductsController> logger)
        {
            if (null == productsRepo)
                throw new ArgumentNullException(nameof(productsRepo));
            if (null == logger)
                throw new ArgumentNullException(nameof(logger));
            _productsRepo = productsRepo;
            _logger = logger;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(int? page, int? pageSize)
        {
            try
            {
                var products = await _productsRepo.ReadAsync(page, pageSize);
                return OkOrNotFound(products);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, ex, "unable to read products");
                return BadRequest("Unable to read products");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _productsRepo.ReadOne(id);
                return OkOrNotFound(product);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, ex, $"unable to read product by id {id}");
                return BadRequest();
            }
        }

        private IActionResult OkOrNotFound<T>(T value)
        {
            if (null == value)
                return NotFound();
            return Ok(value);
        }
    }

}
