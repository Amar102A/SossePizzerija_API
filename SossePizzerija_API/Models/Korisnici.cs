using System;
using System.Collections.Generic;

namespace SossePizzerija_API.Models;

public partial class Korisnici
{
    public int KorisnikId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Lozinka { get; set; } = null!;

    public DateTime? DatumRegistracije { get; set; }

    public virtual ICollection<CustomPizze> CustomPizzes { get; set; } = new List<CustomPizze>();

    public virtual ICollection<Narudzbe> Narudzbes { get; set; } = new List<Narudzbe>();
}
