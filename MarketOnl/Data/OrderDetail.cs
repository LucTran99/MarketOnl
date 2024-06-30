using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int Quatity { get; set; }

    public int? ProductId { get; set; }

    public decimal Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
