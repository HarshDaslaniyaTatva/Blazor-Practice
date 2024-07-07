

using WebApi.Dtos.Dtos;
using WebApi.Dtos.Response;
using WebApiEntity.Dtos.Request;
using WebApiEntity.Dtos.Response;

namespace WebApi.Service.Interface
{
    public interface IProductService
    {
        Task<FilterProductResponseDto> GetAllProductsAsync(int pagesize, int currentpage, string? sortfield, bool sort, string? searchfield, string? search);
        Task<ProductDto?> GetProductAsync(int id);
        bool AddProduct(ProductRequestDto productDto);
        Task<bool> UpdateProductAsync(int id, ProductRequestDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
