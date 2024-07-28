using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Authorized
{
    public int AuthorId { get; set; }

    public int? RolesId { get; set; }

    public int? CrudId { get; set; }

    public virtual Crud? Crud { get; set; }

    public virtual Role? Roles { get; set; }
}
