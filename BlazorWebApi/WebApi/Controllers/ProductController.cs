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
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Get all products
        [HttpGet]
        public async Task<ActionResult<FilterProductRequestDto>> GetAllProducts(
            [FromQuery] int pagesize = 10,
            [FromQuery] int currentpage = 1,
            [FromQuery] string? sortfield = null,
            [FromQuery] bool sort = true,
            [FromQuery] string? searchfield = null,
            [FromQuery] string? search = null)
        {
            try
            {
                var products = await _productService.GetProductsAsync(pagesize, currentpage, sortfield, sort, searchfield, search);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

            var data = await _productService.GetProductAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // Add a new product
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddProduct(ProductRequestDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var result = await _productService.AddProductAsync(productDto);

            if (result != null)
            {
                return Ok(true);
            }
            else
            {
                // Failed to add product
                return BadRequest(false);
            }
        }



        // Update an existing product
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, ProductRequestDto productDto)
        {
            if (id <= 0 || productDto == null)
            {
                return BadRequest();
            }

            var updateResult = await _productService.UpdateProductAsync(id, productDto);

            if (!updateResult)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete a product
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var deleteResult = await _productService.DeleteProductAsync(id);

            if (!deleteResult)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
