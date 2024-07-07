using WebApi.Dtos.Dtos;
using WebApi.Entity.DataModels;

namespace WebApi.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> DeleteAsync(int id);
        IQueryable<ProductDto> GetProducts();

    }
}
