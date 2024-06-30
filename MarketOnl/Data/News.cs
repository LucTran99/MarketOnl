using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class News
{
    public int NewsId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Alias { get; set; }

    public string? Detail { get; set; }

    public string? Image { get; set; }

    public int? Views { get; set; }

    public int? NewCatId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdateBy { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public virtual NewsCat? NewCat { get; set; }
}
