using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiEntity.DataModels;

[Table("products")]
public partial class Product
{
    [Key]
    [Column("product_id")]
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

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
