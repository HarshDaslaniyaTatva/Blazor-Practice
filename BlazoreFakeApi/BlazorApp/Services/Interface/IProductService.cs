
using BlazorApp.Models;

namespace BlazorApp.Services.Interface;
public interface IProductService
{
    public Task<List<ProductModels>> GetProduct();
    Task<ProductModels?> GetProduct(int id);
    Task AddProduct(ProductModels product);
    Task UpdateProduct(ProductModels product);
    Task DeleteProduct(int id);
}