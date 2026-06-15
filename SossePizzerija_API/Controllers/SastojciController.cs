using Microsoft.AspNetCore.Mvc;
using SossePizzerija_API.Models;

namespace SossePizzerija_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SastojciController : ControllerBase
    {
        private readonly SossePizzerijaContext _db;

        public SastojciController(SossePizzerijaContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Sastojci> sastojci = _db.Sastojcis.ToList();
            return Ok(sastojci);
        }
    }
}