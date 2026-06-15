using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class CustomPizzaSastojci
{
    public int Id { get; set; }

    public int? CustomPizzaId { get; set; }

    public int? SastojakId { get; set; }

    public virtual CustomPizze? CustomPizza { get; set; }

    public virtual Sastojci? Sastojak { get; set; }
}
