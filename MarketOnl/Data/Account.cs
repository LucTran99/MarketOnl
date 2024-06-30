using System;
using System.Collections.Generic;

namespace MarketOnl.Data;

public partial class Account
{
    public int AccountId { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public string Active { get; set; } = null!;

    public string? Picture { get; set; }

    public int? RoleId { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Role? Role { get; set; }
}
