using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SossePizzerija_API.Models;

namespace SossePizzerija_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        private readonly SossePizzerijaContext _db;

        public KorisniciController(SossePizzerijaContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Korisnici> korisnici = _db.Korisnicis.ToList();
            return Ok(korisnici);
        }

        [HttpPost]
        public IActionResult Registracija([FromBody] Korisnici korisnik)
        {
            // Provjeri da li email već postoji
            bool emailPostoji = _db.Korisnicis.Any(k => k.Email == korisnik.Email);
            if (emailPostoji)
                return BadRequest("Email već postoji!");

            // Provjeri da li username već postoji
            bool usernamePostoji = _db.Korisnicis.Any(k => k.Username == korisnik.Username);
            if (usernamePostoji)
                return BadRequest("Username već postoji!");

            _db.Add(korisnik);
            _db.SaveChanges();
            return Ok(korisnik.KorisnikId);
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            Korisnici? korisnik = _db.Korisnicis
                .Where(k => k.Username == request.Username && k.Lozinka == request.Lozinka)
                .FirstOrDefault();

            if (korisnik == null)
                return Unauthorized("Pogrešni podaci!");

            return Ok(korisnik);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Lozinka { get; set; } = string.Empty;
    }
}