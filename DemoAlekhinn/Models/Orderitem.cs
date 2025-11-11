using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Orderitem
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Materialid { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Customerorder Order { get; set; } = null!;
}
