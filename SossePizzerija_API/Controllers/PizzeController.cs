using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SossePizzerija_API.Models;

namespace SossePizzerija_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzeController : ControllerBase
    {
        private readonly SossePizzerijaContext _db;

        public PizzeController(SossePizzerijaContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Pizze> pizze = _db.Pizzes.ToList();
            return Ok(pizze);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Pizze? pizza = _db.Pizzes.Where(p => p.PizzaId == id).FirstOrDefault();
            if (pizza == null) return NotFound();
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Dodaj([FromBody] Pizze pizza)
        {
            _db.Add(pizza);
            _db.SaveChanges();
            return Ok(pizza.PizzaId);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Obrisi(int id)
        {
            Pizze? pizza = _db.Pizzes.Where(p => p.PizzaId == id).FirstOrDefault();
            if (pizza == null) return NotFound();
            _db.Remove(pizza);
            _db.SaveChanges();
            return Ok("Obrisano");
        }
    }
}