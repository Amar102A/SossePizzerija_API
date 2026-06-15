using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class CustomPizze
{
    public int CustomPizzaId { get; set; }

    public int? KorisnikId { get; set; }

    public string? Naziv { get; set; }

    public string? Velicina { get; set; }

    public string? Tijesto { get; set; }

    public string? Sos { get; set; }

    public decimal? UkupnaCijena { get; set; }

    public DateTime? DatumKreiranja { get; set; }

    public virtual ICollection<CustomPizzaSastojci> CustomPizzaSastojcis { get; set; } = new List<CustomPizzaSastojci>();

    public virtual Korisnici? Korisnik { get; set; }
}
