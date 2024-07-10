using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public int? CatId { get; set; }

    public decimal? Price { get; set; }

    public decimal? PriceSale { get; set; }

    public int? Quatity { get; set; }

    public string? Picture { get; set; }

    public string? Alias { get; set; }

    public string? Detail { get; set; }

    public int? ViewCount { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public virtual CatProduct? Cat { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
