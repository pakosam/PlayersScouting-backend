using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Services
{
    public interface IRatingsService
    {
        Task<RatingDto> CreateRatings(CreateRatingDto createdRating);
        Task<RatingDto> UpdateRatings(UpdateRatingDto updatedRating);
        Task<List<RatingDto>> GetAllRatings();
        Task<RatingDto> GetRating(int id);
        Task<RatingDto> DeleteRating(int id);
        Task<RatingDto> GetRatingByPlayerId(int playerId);

    }
}
