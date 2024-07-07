using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Dtos.Dtos;

namespace WebApi.Dtos.Response
{
 
    public class FilterProductResponseDto
    {
        public List<ProductDto> ProductList { get; set; } = [];
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
