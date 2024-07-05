using BlazorWebApp.Dtos;
using BlazorWebApp.Services.Interface;
using System.Text.Json;

namespace BlazorWebApp.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        private string ConstructUrl(int pagesize, int currentpage, string? sortfield, bool sort, string? searchfield, string? search)
        {
            var queryParams = new List<string>();

            if (pagesize > 0)
            {
                queryParams.Add($"pagesize={pagesize}");
            }

            if (currentpage > 0)
            {
                queryParams.Add($"currentpage={currentpage}");
            }

            if (!string.IsNullOrEmpty(sortfield))
            {
                queryParams.Add($"sortfield={sortfield}");
            }

            queryParams.Add($"sort={sort}");

            if (!string.IsNullOrEmpty(searchfield))
            {
                queryParams.Add($"searchfield={searchfield}");
            }

            if (!string.IsNullOrEmpty(search))
            {
                queryParams.Add($"search={search}");
            }

            string queryString = string.Join("&", queryParams);
            return $"api/product?{queryString}";
        }

        public async Task<FilterProductRequestDto> GetProduct(int pagesize, int currentpage, string? sortfield, bool sort, string? searchfield, string? search)
        {
            try
            {
                string url = ConstructUrl(pagesize, currentpage, sortfield, sort, searchfield, search);
                FilterProductRequestDto? products = await _httpClient.GetFromJsonAsync<FilterProductRequestDto>(url, _serializerOptions);
                return products ?? new();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        public async Task<GetProductDtos?> GetProduct(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GetProductDtos>($"api/product/{id}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<ResponseDto<bool>> AddProduct(SetProductDto productDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/product", productDto, _serializerOptions);

                if (response.IsSuccessStatusCode)
                {
                    return new ResponseDto<bool>
                    {
                        Success = true,
                        Message = "Product created successfully.",
                        Data = true
                    };
                }
                else
                {
                    return new ResponseDto<bool>
                    {
                        Success = false,
                        Message = "Failed to create product. Status code: " + response.StatusCode,
                        Data = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return new ResponseDto<bool>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = false
                };
            }
        }





        public async Task<ResponseDto<bool>> UpdateProduct(GetProductDtos productDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/product/{productDto.ProductId}", productDto, _serializerOptions);
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseDto<bool>
                    {
                        Success = true,
                        Message = "Product updated successfully.",
                        Data = true
                    };
                }
                else
                {
                    return new ResponseDto<bool>
                    {
                        Success = false,
                        Message = "Failed to update product.",
                        Data = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseDto<bool>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

        public async Task<ResponseDto<bool>> DeleteProduct(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseDto<bool>
                    {
                        Success = true,
                        Message = "Product deleted successfully.",
                        Data = true
                    };
                }
                else
                {
                    return new ResponseDto<bool>
                    {
                        Success = false,
                        Message = "Failed to delete product.",
                        Data = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseDto<bool>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

      
    }
}
