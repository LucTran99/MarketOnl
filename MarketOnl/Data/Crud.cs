using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Crud
{
    public int CrudId { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Authorized> Authorizeds { get; set; } = new List<Authorized>();

    public virtual Category? Category { get; set; }
}
