using BlazorWebApp.Dtos;

namespace BlazorWebApp.Services.Interface
{
    public interface IProductService
    {
        public Task<List<GetProductDtos>> GetProduct();
        Task<GetProductDtos?> GetProduct(int id);
        Task AddProduct(GetProductDtos product);
        Task UpdateProduct(GetProductDtos product);
        Task DeleteProduct(int id);
    }
}
