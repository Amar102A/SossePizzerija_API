using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class Sastojci
{
    public int SastojakId { get; set; }

    public string Naziv { get; set; } = null!;

    public decimal Cijena { get; set; }

    public virtual ICollection<CustomPizzaSastojci> CustomPizzaSastojcis { get; set; } = new List<CustomPizzaSastojci>();
}
