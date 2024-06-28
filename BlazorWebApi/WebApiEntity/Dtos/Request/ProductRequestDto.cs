using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEntity.Dtos.Request
{
    public class ProductRequestDto
    {
        public int ProductId { get; set; }

        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        [Column("stock_quantity")]
        public int StockQuantity { get; set; }
    }

    public class FilterProductRequestDto
    {
        public List<ProductRequestDto> ProductList { get; set; } = [];
        public int  TotalPage {  get; set; } 
        public int  PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
