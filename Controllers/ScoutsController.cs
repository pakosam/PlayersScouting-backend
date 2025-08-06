using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.DTOs;
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
        public async Task<ActionResult<Scout>> AddScout(CreateScoutDto scout)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == scout.PlayerFullName);

            if (player == null)
            {
                return NotFound();
            }

            var createScout = new Scout
            {
                Name = scout.Name,
                Surname = scout.Surname,
                Birthdate = scout.Birthdate,
                Birthplace = scout.Birthplace,
                Age = scout.Age,
                Email = scout.Email,
                Password = scout.Password,
                PlayerId = player.Id
            };

            _context.Scouts.Add(createScout);
            await _context.SaveChangesAsync();

            return createScout;
        }

        [HttpPut]
        public async Task<ActionResult<Scout>> UpdateScout(UpdateScoutDto updatedScout)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == updatedScout.PlayerFullName);

            if (player == null)
            {
                return NotFound();
            }

            var dbScout = await _context.Scouts.FindAsync(updatedScout.Id);

            if (dbScout == null)
            {
                return NotFound();
            }

            dbScout.Name = updatedScout.Name;
            dbScout.Surname = updatedScout.Surname;
            dbScout.Birthdate = updatedScout.Birthdate;
            dbScout.Birthplace = updatedScout.Birthplace;
            dbScout.Age = updatedScout.Age;
            dbScout.Email = updatedScout.Email;
            dbScout.Password = updatedScout.Password;
            dbScout.PlayerId = player.Id;

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
