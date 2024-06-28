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
        public async Task<List<GetProductDtos>> GetProduct()
        {
            try
            {
                var apijson = await _httpClient.GetStreamAsync("api/product");
                var products = await JsonSerializer.DeserializeAsync<List<GetProductDtos>>(apijson, _serializerOptions);
                return products;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<GetProductDtos?> GetProduct(int id)
        {
            return await _httpClient.GetFromJsonAsync<GetProductDtos>($"product/{id}");
        }
        public async Task AddProduct(GetProductDtos product)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("products", product);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"products/{id}");
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        public async Task UpdateProduct(GetProductDtos product)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"products/{product.ProductId}", product);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
