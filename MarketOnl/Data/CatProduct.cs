﻿using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class CatProduct
{
    public int CatId { get; set; }

    public string? CatName { get; set; }

    public string? Description { get; set; }

    public bool? Published { get; set; }

    public string? Thumb { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
