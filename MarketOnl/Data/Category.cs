using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ParentId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Crud> Cruds { get; set; } = new List<Crud>();
}
