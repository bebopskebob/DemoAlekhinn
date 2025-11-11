using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Production
{
    public int Id { get; set; }

    public int Materialid { get; set; }

    public string Code { get; set; } = null!;

    public decimal Quantity { get; set; }

    public virtual Material Material { get; set; } = null!;
}
