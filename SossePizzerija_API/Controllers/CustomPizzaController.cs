using Microsoft.AspNetCore.Mvc;
using SossePizzerija_API.Models;

namespace SossePizzerija_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomPizzaController : ControllerBase
    {
        private readonly SossePizzerijaContext _db;

        public CustomPizzaController(SossePizzerijaContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Spremi([FromBody] CustomPizze pizza)
        {
            _db.Add(pizza);
            _db.SaveChanges();
            return Ok(pizza.CustomPizzaId);
        }

        [HttpGet("{korisnikId:int}")]
        public IActionResult GetByKorisnik(int korisnikId)
        {
            List<CustomPizze> pizze = _db.CustomPizzes
                .Where(p => p.KorisnikId == korisnikId)
                .ToList();
            return Ok(pizze);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Obrisi(int id)
        {
            CustomPizze? pizza = _db.CustomPizzes
                .Where(p => p.CustomPizzaId == id)
                .FirstOrDefault();
            if (pizza == null) return NotFound();
            _db.Remove(pizza);
            _db.SaveChanges();
            return Ok("Obrisano");
        }
    }
}