using System.Text.Json;
using BlazorApp.Models;
using BlazorApp.Services.Interface;

namespace BlazorApp.Services.Implementation;
public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true,
        };
    }

    
    public async Task<List<ProductModels>> GetProduct()
    {
        try
        { 
            var apijson = await _httpClient.GetStreamAsync("products");
            var products = await JsonSerializer.DeserializeAsync<List<ProductModels>>(apijson ,_serializerOptions);
            return products;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<ProductModels?> GetProduct(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductModels>($"products/{id}");
    }
    public async Task AddProduct(ProductModels product)
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

    public async Task UpdateProduct(ProductModels product)
    {
        try
        {
            await _httpClient.PutAsJsonAsync($"products/{product.Id}", product);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
 

}