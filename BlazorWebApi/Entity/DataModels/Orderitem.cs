using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataModels;

[Table("orderitems")]
public partial class Orderitem
{
    [Key]
    [Column("order_item_id")]
    public int OrderItemId { get; set; }

    [Column("order_id")]
    public int? OrderId { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Column("total_price")]
    [Precision(10, 2)]
    public decimal? TotalPrice { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Orderitems")]
    public virtual Order? Order { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Orderitems")]
    public virtual Product? Product { get; set; }
}
