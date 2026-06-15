using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class StavkeNarudzbe
{
    public int StavkaId { get; set; }

    public int? NarudzbаId { get; set; }

    public int? PizzaId { get; set; }

    public int Kolicina { get; set; }

    public decimal Cijena { get; set; }

    public virtual Narudzbe? Narudzbа { get; set; }

    public virtual Pizze? Pizza { get; set; }
}
