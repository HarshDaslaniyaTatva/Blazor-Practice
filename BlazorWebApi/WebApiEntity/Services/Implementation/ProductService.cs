using Microsoft.EntityFrameworkCore;
using WebApiEntity.DataContext;
using WebApiEntity.DataModels;
using WebApiEntity.Dtos.Request;
using WebApiEntity.Services.Interface;

namespace WebApiEntity.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FilterProductRequestDto> GetProductsAsync(int pagesize, int currentpage, string sortfield, bool sort, string searchfield, string search)
        {
            try
            {
                

                var product = _dbContext.Products.AsQueryable();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    switch (searchfield.ToLower())
                    {
                        case "name":
                            product = product.Where(x => x.Name.ToLower().Contains(search.ToLower()));
                            break;
                        case "description":
                            product = product.Where(x => x.Description.ToLower().Contains(search.ToLower()));
                            break;
                        case "price":
                            product = product.Where(x => x.Price.ToString().Contains(search));
                            break;
                        case "stockquantity":
                            product = product.Where(x => x.StockQuantity.ToString().Contains(search));
                            break;
                        default:
                            break;
                    }
                }
                switch (sortfield.ToLower())
                {
                    case "name":
                        product = sort ? product.OrderBy(x => x.Name) : product.OrderByDescending(x => x.Name);
                        break;
                    case "description":
                        product = sort ? product.OrderBy(x => x.Description) : product.OrderByDescending(x => x.Description);
                        break;
                    case "price":
                        product = sort ? product.OrderBy(x => x.Price) : product.OrderByDescending(x => x.Price);
                        break;
                    case "stockquantity":
                        product = sort ? product.OrderBy(x => x.StockQuantity) : product.OrderByDescending(x => x.StockQuantity);
                        break;
                    default:
                        break;
                }

                int totalItem = product.Count();
                pagesize = pagesize <= 0 ? 10 : pagesize;
                int totalPage = (int)Math.Ceiling((double)totalItem / pagesize);
                currentpage = (currentpage <= 0 || currentpage > totalPage) ? 1 : currentpage;



                List<ProductRequestDto> products = await product.Select(x => new ProductRequestDto
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity,
                }).Skip((currentpage - 1)*pagesize).Take(pagesize).ToListAsync();

                FilterProductRequestDto filterData = new()
                {
                    ProductList = products,
                    TotalPage = totalPage,
                    PageSize = pagesize,
                    CurrentPage = currentpage,
                };

                return filterData;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }
        public async Task<ProductRequestDto?> GetProductAsync(int id)
        {
            try
            {
                Product? data = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

                if (data == null)
                {
                    return null;
                }

                ProductRequestDto result = new()
                {
                    ProductId = data.ProductId,
                    Name = data.Name,
                    Description = data.Description,
                    Price = data.Price,
                    StockQuantity = data.StockQuantity,
                };

                return result;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }
    }
}
