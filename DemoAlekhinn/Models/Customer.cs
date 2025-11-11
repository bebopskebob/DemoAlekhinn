using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Inn { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public bool Issalesman { get; set; }

    public bool Isbuyer { get; set; }

    public virtual ICollection<Customerorder> Customerorders { get; set; } = new List<Customerorder>();
}
