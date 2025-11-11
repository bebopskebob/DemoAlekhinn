using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Productspecification
{
    public int Id { get; set; }

    public string Productcode { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public int Materialid { get; set; }

    public decimal Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;
}
