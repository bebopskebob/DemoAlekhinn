using System;
using System.Collections.Generic;

namespace DemoAlekhinn.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public virtual ICollection<Costcalculation> Costcalculations { get; set; } = new List<Costcalculation>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Price? Price { get; set; }

    public virtual ICollection<Production> Productions { get; set; } = new List<Production>();

    public virtual ICollection<Productspecification> Productspecifications { get; set; } = new List<Productspecification>();

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();
}
