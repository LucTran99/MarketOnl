using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class NewsCat
{
    public int NewCatId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
