using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DataContext _context;
        public PlayersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAllPlayers()
        {
            var players = await _context.Players.ToListAsync();

            if (players == null)
            {
                return NotFound();
            }

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetSinglePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer(Player player)
        {
            var createPlayer = new Player
            {
                Name = player.Name,
                Surname = player.Surname,
                Birthdate = player.Birthdate,
                Birthplace = player.Birthplace,
                Age = player.Age,
                Height = player.Height,
                Foot = player.Foot,
                ShirtNumber = player.ShirtNumber,
                Positions = player.Positions,
                Club = player.Club
            };

            _context.Players.Add(createPlayer);
            await _context.SaveChangesAsync();

            return createPlayer;
        }

        [HttpPut]
        public async Task<ActionResult<Player>> UpdatePlayer(Player player)
        {
            if (player.Id == null)
            {
                return BadRequest();
            }

            var dbPlayer = await _context.Players.FindAsync(player.Id);

            if (dbPlayer == null)
            {
                return NotFound();
            }

            dbPlayer.Name = player.Name;
            dbPlayer.Surname = player.Surname;
            dbPlayer.Birthdate = player.Birthdate;
            dbPlayer.Birthplace = player.Birthplace;
            dbPlayer.Age = player.Age;
            dbPlayer.Height = player.Height;
            dbPlayer.Foot = player.Foot;
            dbPlayer.ShirtNumber = player.ShirtNumber;
            dbPlayer.Positions = player.Positions;
            dbPlayer.Club = player.Club;

            await _context.SaveChangesAsync();

            return dbPlayer;
        }

        [HttpDelete]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }
    }
}