using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoutsController : ControllerBase
    {
        private readonly DataContext _context;
        public ScoutsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Scout>>> GetAllScouts()
        {
            var scouts = await _context.Scouts.ToListAsync();

            if (scouts == null)
            {
                return NotFound();
            }

            return Ok(scouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Scout>> GetSingleScout(int id)
        {
            var scout = await _context.Scouts.FindAsync(id);

            if (scout == null)
            {
                return NotFound();
            }

            return scout;
        }

        [HttpPost]
        public async Task<ActionResult<Scout>> AddScout(Scout scout)
        {
            var player = await _context.Players.FindAsync(scout.PlayerId);

            if (player == null)
            {
                return NotFound();
            }

            var createScout = new Scout
            {
                Id = scout.Id,
                Name = scout.Name,
                Surname = scout.Surname,
                Birthdate = scout.Birthdate,
                Birthplace = scout.Birthplace,
                Age = scout.Age,
                Email = scout.Email,
                Password = scout.Password,
                PlayerId = scout.PlayerId
            };

            _context.Scouts.Add(createScout);
            await _context.SaveChangesAsync();

            return createScout;
        }

        [HttpPut]
        public async Task<ActionResult<Scout>> UpdateScout(Scout scout)
        {
            var player = await _context.Players.FindAsync(scout.PlayerId);

            if (player == null)
            {
                return NotFound();
            }

            var dbScout = await _context.Scouts.FindAsync(scout.Id);

            if (dbScout == null)
            {
                return NotFound();
            }

            dbScout.Name = scout.Name;
            dbScout.Surname = scout.Surname;
            dbScout.Birthdate = scout.Birthdate;
            dbScout.Birthplace = scout.Birthplace;
            dbScout.Age = scout.Age;
            dbScout.Email = scout.Email;
            dbScout.Password = scout.Password;
            dbScout.PlayerId = scout.PlayerId;

            await _context.SaveChangesAsync();

            return dbScout;
        }

        [HttpDelete]
        public async Task<ActionResult<Scout>> DeleteScout(int id)
        {
            var scout = await _context.Scouts.FindAsync(id);

            if (scout == null)
            {
                return NotFound();
            }

            _context.Scouts.Remove(scout);
            await _context.SaveChangesAsync();

            return scout;
        }
    }
}
