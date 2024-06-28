using Microsoft.AspNetCore.Mvc;
using WebApiEntity.DataContext;
using WebApiEntity.Dtos.Request;
using WebApiEntity.Services.Implementation;
using WebApiEntity.Services.Interface;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _product;

        public ProductController(IProductService product)
        {
            _product = product;
        }

        // Get all products
        [HttpGet("{pageSize:int}/{currentPage:int}/{sortField}/{sort:bool}/{searchField}/{search}")]
        public async Task<ActionResult<FilterProductRequestDto>> GetAllProducts(int pageSize,int currentPage,string sortField,bool sort,string searchField,string search)
        {
            FilterProductRequestDto products = await _product.GetProductsAsync(pageSize, currentPage, sortField, sort, searchField, search);
            return Ok(products);
        }

        // Get product by id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductRequestDto>> GetProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            ProductRequestDto? data = await _product.GetProductAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}
