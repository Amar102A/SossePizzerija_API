using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SossePizzerija_API.Models;

namespace SossePizzerija_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NarudzbeController : ControllerBase
    {
        private readonly SossePizzerijaContext _db;

        public NarudzbeController(SossePizzerijaContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult KreirajNarudzbu([FromBody] NarudzbaRequest request)
        {
            var narudzba = new Narudzbe
            {
                KorisnikId = request.KorisnikId,
                UkupnaCijena = request.UkupnaCijena,
                Status = "Na čekanju"
            };

            _db.Add(narudzba);
            _db.SaveChanges();

            foreach (var stavka in request.Stavke)
            {
                // Preskoči custom pizze
                if (stavka.PizzaId == 999) continue;

                var s = new StavkeNarudzbe
                {
                    NarudzbаId = narudzba.NarudzbаId,
                    PizzaId = stavka.PizzaId,
                    Kolicina = stavka.Kolicina,
                    Cijena = stavka.Cijena
                };
                _db.Add(s);
            }

            _db.SaveChanges();
            return Ok(narudzba.NarudzbаId);
        }

        [HttpGet("{korisnikId:int}")]
        public IActionResult GetByKorisnik(int korisnikId)
        {
            var narudzbe = _db.Narudzbes
                .Where(n => n.KorisnikId == korisnikId)
                .OrderByDescending(n => n.DatumNarudzbe)
                .ToList();
            return Ok(narudzbe);
        }

        [HttpDelete("{id:int}")]
        public IActionResult ObrisiNarudzbu(int id)
        {
            // Prvo obriši stavke
            var stavke = _db.StavkeNarudzbes
                .Where(s => s.NarudzbаId == id)
                .ToList();
            _db.RemoveRange(stavke);

            // Zatim obriši narudžbu
            Narudzbe? narudzba = _db.Narudzbes
                .Where(n => n.NarudzbаId == id)
                .FirstOrDefault();
            if (narudzba == null) return NotFound();
            _db.Remove(narudzba);
            _db.SaveChanges();
            return Ok("Obrisano");
        }
    }


    public class NarudzbaRequest
    {
        public int KorisnikId { get; set; }
        public decimal UkupnaCijena { get; set; }
        public List<StavkaRequest> Stavke { get; set; } = new();
    }

    public class StavkaRequest
    {
        public int PizzaId { get; set; }
        public int Kolicina { get; set; }
        public decimal Cijena { get; set; }
    }
}