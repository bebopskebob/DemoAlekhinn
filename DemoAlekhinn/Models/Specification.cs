using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Specification
{
    public int Id { get; set; }

    public int Materialid { get; set; }

    public decimal Quantity { get; set; }

    public virtual Material Material { get; set; } = null!;
}
