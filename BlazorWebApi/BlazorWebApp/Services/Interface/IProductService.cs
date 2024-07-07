using BlazorWebApp.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Services.Interface
{
    public interface IProductService
    {
        public Task<FilterProductRequestDto> GetProduct(int pagesize, int currentpage, string? sortfield, bool sort, string? searchfield, string? search);
        Task<GetProductDtos?> GetProduct(int id);
        Task<User?> GetUser(string username);
        Task<ResponseDto<bool>> AddProduct(SetProductDto productDto);
        Task<ResponseDto<bool>> UpdateProduct(GetProductDtos productDto);
        Task<ResponseDto<bool>> DeleteProduct(int id);
    }
}
