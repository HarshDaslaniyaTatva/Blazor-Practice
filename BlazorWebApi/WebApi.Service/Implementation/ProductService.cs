

using Microsoft.EntityFrameworkCore;
using WebApi.Dtos.Dtos;
using WebApi.Dtos.Response;
using WebApi.Entity.DataModels;
using WebApi.Repository.Interface;
using WebApi.Service.Interface;
using WebApiEntity.Dtos.Request;

namespace WebApi.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public async Task<FilterProductResponseDto> GetAllProductsAsync(int pagesize, int currentpage, string? sortfield, bool sort, string? searchfield, string? search)
        {
            try
            {


                var query = _productRepository.GetProducts();

                searchfield = searchfield ?? string.Empty;
                sortfield = sortfield ?? string.Empty;
                search = search == null ? string.Empty : search.Trim();

                if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(searchfield))
                {
                    var propertyInfo = typeof(ProductDto).GetProperty(searchfield);
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
                    if (typeof(ProductDto).GetProperty(sortfield) == null)
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
                    .ToListAsync();

                FilterProductResponseDto filterData = new()
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

        public async Task<ProductDto> GetProductAsync(int id)
        {
            try
            {
                ProductDto? data = await _productRepository.GetProducts().FirstOrDefaultAsync(x => x.ProductId == id);

                if (data == null)
                {
                    return new();
                }
                return data;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public bool AddProduct(ProductRequestDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return false;

                }

                Product product = new()
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    StockQuantity = productDto.StockQuantity,
                };

                _productRepository.Insert(product);
                _productRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                return await _productRepository.DeleteAsync(id);
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
                ProductDto? productData = await _productRepository.GetProducts().FirstOrDefaultAsync(x => x.ProductId == id);

                if (productData == null)
                {
                    return false;
                }

                Product product = new()
                {
                    ProductId = productData.ProductId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    StockQuantity = productDto.StockQuantity,
                    CreatedAt = productData.CreatedAt,
                };

                _productRepository.Update(product);
                _productRepository.Save();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Internal Server Error");
            }
        }
    }
}
