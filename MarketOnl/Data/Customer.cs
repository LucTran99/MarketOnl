using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerPassoword { get; set; }

    public DateTime? CreatedDate { get; set; }
}
