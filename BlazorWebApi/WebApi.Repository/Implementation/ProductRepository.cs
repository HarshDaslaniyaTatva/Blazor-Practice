

using Microsoft.EntityFrameworkCore;
using WebApi.Dtos.Dtos;
using WebApi.Entity.DataContext;
using WebApi.Entity.DataModels;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                if(id <= 0) return false;

                Product? data = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
                
                if(data == null) return false;
               
                data.IsDeleted = true;
                Update(data);
                Save();
                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
        IQueryable<ProductDto> IProductRepository.GetProducts()
        {
            return _context.Products.Where(x=> x.IsDeleted == false).Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CreatedAt = p.CreatedAt,
                IsDeleted = p.IsDeleted
            }); 
        }
    }
}
