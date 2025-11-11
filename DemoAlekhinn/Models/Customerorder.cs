using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Customerorder
{
    public int Id { get; set; }

    public string Customerid { get; set; } = null!;

    public DateOnly? Orderdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
