using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using PlayersScouting_backend.DTOs;
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
        public async Task<ActionResult<Ratings>> AddRating(CreateRatingDto rating)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == rating.FullName);

            if (player == null)
            {
                return NotFound();
            }

            var existingRating = _context.Ratings.FirstOrDefault(r => r.PlayerId == player.Id);

            if (existingRating != null)
            {
                return Conflict("This player already has a rating.");
            }

            if (rating.Attack < 1 || rating.Attack > 10 ||
                rating.Defense < 1 || rating.Defense > 10 ||
                rating.Tactics < 1 || rating.Tactics > 10 ||
                rating.Technique < 1 || rating.Technique > 10 ||
                rating.PhysicalStrength < 1 || rating.PhysicalStrength > 10 ||
                rating.MentalStrength < 1 || rating.MentalStrength > 10)
            {
                return BadRequest("All ratings must be between 1 and 10.");
            }

            var createRating = new Ratings
            {
                Attack = rating.Attack,
                Defense = rating.Defense,
                Tactics = rating.Tactics,
                Technique = rating.Technique,
                PhysicalStrength = rating.PhysicalStrength,
                MentalStrength = rating.MentalStrength,
                PlayerId = player.Id
            };

            _context.Ratings.Add(createRating);
            await _context.SaveChangesAsync();

            return createRating;
        }

        [HttpPut]
        public async Task<ActionResult<Ratings>> UpdateRating(UpdateRatingDto updatedRating)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == updatedRating.FullName);

            if (player == null)
            {
                return NotFound();
            }

            var dbRating = await _context.Ratings.FindAsync(updatedRating.Id);

            if (dbRating == null)
            {
                return NotFound();
            }

            if (updatedRating.Attack < 1 || updatedRating.Attack > 10 ||
                updatedRating.Defense < 1 || updatedRating.Defense > 10 ||
                updatedRating.Tactics < 1 || updatedRating.Tactics > 10 ||
                updatedRating.Technique < 1 || updatedRating.Technique > 10 ||
                updatedRating.PhysicalStrength < 1 || updatedRating.PhysicalStrength > 10 ||
                updatedRating.MentalStrength < 1 || updatedRating.MentalStrength > 10)
            {
                return BadRequest("All ratings must be between 1 and 10.");
            }

            dbRating.Attack = updatedRating.Attack;
            dbRating.Defense = updatedRating.Defense;
            dbRating.Tactics = updatedRating.Tactics;
            dbRating.Technique = updatedRating.Technique;
            dbRating.PhysicalStrength = updatedRating.PhysicalStrength;
            dbRating.MentalStrength = updatedRating.MentalStrength;
            dbRating.PlayerId = player.Id;

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
