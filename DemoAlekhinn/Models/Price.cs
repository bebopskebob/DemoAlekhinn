using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Price
{
    public int Materialid { get; set; }

    public decimal Price1 { get; set; }

    public virtual Material Material { get; set; } = null!;
}
