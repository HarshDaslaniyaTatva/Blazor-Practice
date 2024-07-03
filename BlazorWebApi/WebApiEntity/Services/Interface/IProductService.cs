using WebApiEntity.Dtos.Request;

namespace WebApiEntity.Services.Interface
{
    public interface IProductService
    {
        Task<FilterProductRequestDto> GetProductsAsync(int pagesize , int currentpage , string? sortfield , bool sort , string? searchfield , string? search);
        Task<ProductRequestDto?> GetProductAsync(int id);
        Task<ProductRequestDto> AddProductAsync(ProductRequestDto productDto);
        Task<bool> UpdateProductAsync(int id, ProductRequestDto productDto);
        Task<bool> DeleteProductAsync(int id);


    }
}
