using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebApp.Dtos
{
    public class SetProductDto
    {
        [Column("name")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Column("price")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        [Column("stock_quantity")]
        [Required(ErrorMessage = "Stock Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }
    }
}
