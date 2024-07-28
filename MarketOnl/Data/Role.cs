using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Authorized> Authorizeds { get; set; } = new List<Authorized>();
}
