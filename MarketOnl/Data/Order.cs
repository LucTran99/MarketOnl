using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public string? Code { get; set; }

    public string? CustomerName { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? Quantity { get; set; }

    public int? TypePayment { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
