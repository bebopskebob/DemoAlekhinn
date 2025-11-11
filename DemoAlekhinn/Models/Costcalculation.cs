using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Costcalculation
{
    public int Id { get; set; }

    public int Materialid { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Cost { get; set; }

    public DateOnly? Calculationdate { get; set; }

    public virtual Material Material { get; set; } = null!;
}
