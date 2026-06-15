using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class Pizze
{
    public int PizzaId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public decimal Cijena { get; set; }

    public string? Slika { get; set; }

    public virtual ICollection<StavkeNarudzbe> StavkeNarudzbes { get; set; } = new List<StavkeNarudzbe>();
}
