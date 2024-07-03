using Microsoft.EntityFrameworkCore;
using WebApiEntity.DataContext;
using WebApiEntity.DataModels;
using WebApiEntity.Dtos.Request;
using WebApiEntity.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiEntity.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FilterProductRequestDto> GetProductsAsync(int pagesize, int currentpage, string? sortfield, bool sort, string? searchfield, string? search)
        {
            try
            {
                

                var query = _dbContext.Products.AsQueryable();

                searchfield = searchfield ?? string.Empty;
                sortfield = sortfield ?? string.Empty;
                search = search == null ? string.Empty : search.Trim();

                if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(searchfield))
                {
                    var propertyInfo = typeof(Product).GetProperty(searchfield);
                    if (propertyInfo == null)
                    {
                        throw new ArgumentException($"Invalid search field: {searchfield}");
                    }

                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        query = query.Where(p => EF.Property<string>(p, searchfield)
                                                    .ToLower()
                                                    .Contains(search.ToLower()));
                    }
                    else if (propertyInfo.PropertyType == typeof(int)) 
                    {
                        if (int.TryParse(search, out int searchInt))
                        {
                            query = query.Where(p => EF.Property<int>(p, searchfield) == searchInt);
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid search value for {searchfield}: {search}");
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(Decimal))
                    {
                        if (decimal.TryParse(search, out decimal searchInt))
                        {
                            query = query.Where(p => EF.Property<decimal>(p, searchfield) == searchInt);
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid search value for {searchfield}: {search}");
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sortfield))
                {
                    if (typeof(Product).GetProperty(sortfield) == null)
                    {
                        throw new ArgumentException($"Invalid sort field: {sortfield}");
                    }

                    query = sort
                        ? query.OrderBy(p => EF.Property<object>(p, sortfield))
                        : query.OrderByDescending(p => EF.Property<object>(p, sortfield));
                }

                var totalItems = await query.CountAsync();
                pagesize = pagesize <= 0 ? 10 : pagesize;
                int totalPage = (int)Math.Ceiling((double)totalItems / pagesize);
                currentpage = (currentpage <= 0 || currentpage > totalPage) ? 1 : currentpage;

                var items = await query
                    .Skip((currentpage - 1) * pagesize)
                    .Take(pagesize)
                    .Select(x => new ProductRequestDto
                    {
                        ProductId = x.ProductId,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        StockQuantity = x.StockQuantity,
                    })
                    .ToListAsync();

                FilterProductRequestDto filterData = new()
                {
                    ProductList = items,
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


        public async Task<ProductRequestDto> AddProductAsync(ProductRequestDto productDto)
        {
            try
            {
                Product product = new()
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    StockQuantity = productDto.StockQuantity,
                };

                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                productDto.ProductId = product.ProductId;
                return productDto;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task<bool> UpdateProductAsync(int id, ProductRequestDto productDto)
        {
            try
            {
                Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

                if (product == null)
                {
                    return false;
                }

                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.StockQuantity = productDto.StockQuantity;

                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

                if (product == null)
                {
                    return false;
                }

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }
    }
}
