using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Repositories
{
    public interface IRatingsRepository
    {
        Task CreateRatings(Ratings ratings);
        Task UpdateRatings(Ratings ratings);
        Task<List<Ratings>> GetAllRatings();
        Task<Ratings> GetRating(int id);
        Task DeleteRating(Ratings ratings);
        Task DeleteRatingByPlayerId(int playerId);
        Task<Ratings> GetRatingByPlayerId(int playerId);
    }
}
