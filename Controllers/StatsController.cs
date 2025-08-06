using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly DataContext _context;
        public StatsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stats>>> GetAllStats()
        {
            var stats = await _context.Stats.ToListAsync();

            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stats>> GetSingleStat(int id)
        {
            var stat = await _context.Stats.FindAsync(id);

            if (stat == null)
            {
                return NotFound();
            }

            return stat;
        }

        [HttpPost]
        public async Task<ActionResult<Stats>> AddStat(CreateStatDto stat)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == stat.FullName);

            if (player == null)
            {
                return NotFound();
            }

            var createStat = new Stats
            {
                Season = stat.Season,
                Club = stat.Club,
                MatchesPlayed = stat.MatchesPlayed,
                Goals = stat.Goals,
                Assists = stat.Assists,
                PlayerId = player.Id
            };

            _context.Stats.Add(createStat);
            await _context.SaveChangesAsync();

            return createStat;
        }

        [HttpPut]
        public async Task<ActionResult<Stats>> UpdateStat(UpdateStatDto updatedStat)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == updatedStat.FullName);

            if (player == null)
            {
                return NotFound();
            }

            var dbStat = await _context.Stats.FindAsync(updatedStat.Id);

            if (dbStat == null)
            {
                return NotFound();
            }

            dbStat.Season = updatedStat.Season;
            dbStat.Club = updatedStat.Club;
            dbStat.MatchesPlayed = updatedStat.MatchesPlayed;
            dbStat.Goals = updatedStat.Goals;
            dbStat.Assists = updatedStat.Assists;
            dbStat.PlayerId = player.Id;

            await _context.SaveChangesAsync();

            return dbStat;
        }

        [HttpDelete]
        public async Task<ActionResult<Stats>> DeleteStat(int id)
        {
            var stat = await _context.Stats.FindAsync(id);

            if (stat == null)
            {
                return NotFound();
            }

            _context.Stats.Remove(stat);
            await _context.SaveChangesAsync();

            return stat;
        }
    }
}
