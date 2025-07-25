using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly DataContext _context;
        public RatingsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ratings>>> GetAllRatings()
        {
            var ratings = await _context.Ratings.ToListAsync();

            if (ratings == null)
            {
                return NotFound();
            }

            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ratings>> GetSingleRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        [HttpPost]
        public async Task<ActionResult<Ratings>> AddRating(Ratings rating)
        {
            var player = await _context.Players.FindAsync(rating.PlayerId);

            if (player == null)
            {
                return NotFound();
            }

            var createRating = new Ratings
            {
                Id = rating.Id,
                Attack = rating.Attack,
                Defense = rating.Defense,
                Tactics = rating.Tactics,
                Technique = rating.Technique,
                PhysicalStrength = rating.PhysicalStrength,
                MentalStrength = rating.MentalStrength,
                PlayerId = rating.PlayerId
            };

            _context.Ratings.Add(createRating);
            await _context.SaveChangesAsync();

            return createRating;
        }

        [HttpPut]
        public async Task<ActionResult<Ratings>> UpdateRating(Ratings rating)
        {
            var player = await _context.Players.FindAsync(rating.PlayerId);

            if (player == null)
            {
                return NotFound();
            }

            var dbRating = await _context.Ratings.FindAsync(rating.Id);

            if (dbRating == null)
            {
                return NotFound();
            }

            dbRating.Attack = rating.Attack;
            dbRating.Defense = rating.Defense;
            dbRating.Tactics = rating.Tactics;
            dbRating.Technique = rating.Technique;
            dbRating.PhysicalStrength = rating.PhysicalStrength;
            dbRating.MentalStrength = rating.MentalStrength;
            dbRating.PlayerId = rating.PlayerId;

            await _context.SaveChangesAsync();

            return dbRating;
        }

        [HttpDelete]
        public async Task<ActionResult<Ratings>> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return rating;
        }
    }
}
