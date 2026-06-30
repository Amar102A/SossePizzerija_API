using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class Narudzbe
{
    public int NarudzbаId { get; set; }

    public int? KorisnikId { get; set; }

    public DateTime? DatumNarudzbe { get; set; }

    public decimal UkupnaCijena { get; set; }

    public string? Status { get; set; }

    public string? NacinPlacanja { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public double? DostavljacLatitude { get; set; }

    public double? DostavljacLongitude { get; set; }

    public virtual Korisnici? Korisnik { get; set; }

    public virtual ICollection<StavkeNarudzbe> StavkeNarudzbes { get; set; } = new List<StavkeNarudzbe>();
}
