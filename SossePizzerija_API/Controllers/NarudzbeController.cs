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
                Status = "Na čekanju",
                NacinPlacanja = request.NacinPlacanja,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                // Dostavljač kreće iz pizzerije
                DostavljacLatitude = 43.8476,
                DostavljacLongitude = 18.3564,
            };

            _db.Add(narudzba);
            _db.SaveChanges();

            foreach (var stavka in request.Stavke)
            {
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

        [HttpGet("{id:int}")]
        public IActionResult GetNarudzbaPracenje(int id)
        {
            var narudzba = _db.Narudzbes
                .Where(n => n.NarudzbаId == id)
                .Select(n => new
                {
                    narudzbaId = n.NarudzbаId,
                    n.Status,
                    n.NacinPlacanja,
                    n.Latitude,
                    n.Longitude,
                    n.DostavljacLatitude,
                    n.DostavljacLongitude,
                    n.UkupnaCijena,
                    n.DatumNarudzbe,
                })
                .FirstOrDefault();

            if (narudzba == null) return NotFound();
            return Ok(narudzba);
        }

        [HttpPut("{id:int}")]
        public IActionResult AzurirajLokacijuDostavljaca(int id, [FromBody] LokacijaDostavljacaRequest request)
        {
            var narudzba = _db.Narudzbes.Find(id);
            if (narudzba == null) return NotFound();

            narudzba.DostavljacLatitude = request.Latitude;
            narudzba.DostavljacLongitude = request.Longitude;
            if (!string.IsNullOrEmpty(request.Status))
                narudzba.Status = request.Status;

            _db.SaveChanges();
            return Ok();
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
            var stavke = _db.StavkeNarudzbes
                .Where(s => s.NarudzbаId == id)
                .ToList();
            _db.RemoveRange(stavke);

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
        public string? NacinPlacanja { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class StavkaRequest
    {
        public int PizzaId { get; set; }
        public int Kolicina { get; set; }
        public decimal Cijena { get; set; }
    }

    public class LokacijaDostavljacaRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Status { get; set; }
    }
}
