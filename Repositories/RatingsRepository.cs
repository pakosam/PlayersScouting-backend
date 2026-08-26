using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Repositories
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly DataContext _dataContext;
        public RatingsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task CreateRatings(Ratings ratings)
        {
            _dataContext.Ratings.Add(ratings);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteRating(Ratings ratings)
        {
            _dataContext.Ratings.Remove(ratings);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteRatingByPlayerId(int playerId)
        {
            var ratings = await _dataContext.Ratings
                .Where(r => r.PlayerId == playerId)
                .ToListAsync();

            _dataContext.Ratings.RemoveRange(ratings);
        }

        public async Task<List<Ratings>> GetAllRatings()
        {
            var ratings = await _dataContext.Ratings.ToListAsync();

            return ratings;
        }

        public async Task<Ratings> GetRating(int id)
        {
            var rating = await _dataContext.Ratings.FindAsync(id);

            return rating;
        }

        public Task<Ratings> GetRatingByPlayerId(int playerId)
        {
            var rating = _dataContext.Ratings.FirstOrDefaultAsync(r => r.PlayerId == playerId);

            return rating;
        }

        public async Task UpdateRatings(Ratings ratings)
        {
            _dataContext.Ratings.Update(ratings);

            await _dataContext.SaveChangesAsync();
        }
    }
}
