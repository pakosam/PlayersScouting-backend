using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Repositories;

namespace PlayersScouting_backend.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository _ratingRepository;
        private readonly IPlayerService _playerService;

        public RatingsService(IRatingsRepository ratingRepository, IPlayerService playerService)
        {
            _ratingRepository = ratingRepository;
            _playerService = playerService;
        }
        public async Task<RatingDto> CreateRatings(CreateRatingDto createRatingDto)
        {
            ValidateRatingData(
                createRatingDto.Attack,
                createRatingDto.Defense,
                createRatingDto.Tactics,
                createRatingDto.Technique,
                createRatingDto.PhysicalStrength,
                createRatingDto.MentalStrength
            );

            var player = await _playerService.GetPlayerByFullName(createRatingDto.FullName);

            if (player == null)
            {
                throw new KeyNotFoundException($"Player with name {createRatingDto.FullName} does not exist.");
            }

            var existingRating = await _ratingRepository.GetRatingByPlayerId(player.Id);

            if (existingRating != null)
            {
                throw new InvalidOperationException("This player already has a rating.");
            }

            var createdRating = new Ratings
            {
                Attack = createRatingDto.Attack,
                Defense = createRatingDto.Defense,
                Tactics = createRatingDto.Tactics,
                Technique = createRatingDto.Technique,
                PhysicalStrength = createRatingDto.PhysicalStrength,
                MentalStrength = createRatingDto.MentalStrength,
                PlayerId = player.Id
            };

            await _ratingRepository.CreateRatings(createdRating);

            return new RatingDto
            {
                Attack = createdRating.Attack,
                Defense = createdRating.Defense,
                Tactics = createdRating.Tactics,
                Technique = createdRating.Technique,
                PhysicalStrength = createdRating.PhysicalStrength,
                MentalStrength = createdRating.MentalStrength,
                PlayerId = player.Id
            };
        }

        public async Task<RatingDto> DeleteRating(int id)
        {
            var player = await _playerService.GetPlayer(id);

            if (player == null)
                throw new KeyNotFoundException($"Player with ID {id} not found.");

            var rating = await _ratingRepository.GetRatingByPlayerId(player.Id);

            if (rating == null)
                throw new KeyNotFoundException("Rating not found.");

            await _ratingRepository.DeleteRating(rating);

            return new RatingDto
            {
                Attack = rating.Attack,
                Defense = rating.Defense,
                Tactics = rating.Tactics,
                Technique = rating.Technique,
                PhysicalStrength = rating.PhysicalStrength,
                MentalStrength = rating.MentalStrength
            };
        }

        public async Task<List<RatingDto>> GetAllRatings()
        {
            var ratings = await _ratingRepository.GetAllRatings();

            if (ratings == null)
            {
                throw new Exception($"Ratings don't exist.");
            }

            return ratings.Select(r => new RatingDto
            {
                Attack = r.Attack,
                Defense = r.Defense,
                Tactics = r.Tactics,
                Technique = r.Technique,
                PhysicalStrength = r.PhysicalStrength,
                MentalStrength = r.MentalStrength,
                PlayerId = r.PlayerId
            }).ToList();
        }

        public async Task<RatingDto> GetRating(int id)
        {
            var rating = await _ratingRepository.GetRating(id);

            if (rating == null)
            {
                throw new Exception($"Rating don't exist.");
            }

            return new RatingDto
            {
                Attack = rating.Attack,
                Defense = rating.Defense,
                Tactics = rating.Tactics,
                Technique = rating.Technique,
                PhysicalStrength = rating.PhysicalStrength,
                MentalStrength = rating.MentalStrength,
                PlayerId = rating.PlayerId
            };
        }

        public async Task<RatingDto?> GetRatingByPlayerId(int playerId)
        {
            var rating = await _ratingRepository.GetRatingByPlayerId(playerId);

            if (rating == null)
            {
                return null;
            }

            return new RatingDto
            {
                Attack = rating.Attack,
                Defense = rating.Defense,
                Tactics = rating.Tactics,
                Technique = rating.Technique,
                PhysicalStrength = rating.PhysicalStrength,
                MentalStrength = rating.MentalStrength,
                PlayerId = rating.PlayerId
            };
        }

        public async Task<RatingDto> UpdateRatings(UpdateRatingDto updatedRating)
        {
            var player = await _playerService.GetPlayerByFullName(updatedRating.FullName);

            if (player == null)
            {
                throw new KeyNotFoundException($"Player with name {updatedRating.FullName} does not exist.");
            }

            ValidateRatingData(
                updatedRating.Attack,
                updatedRating.Defense,
                updatedRating.Tactics,
                updatedRating.Technique,
                updatedRating.PhysicalStrength,
                updatedRating.MentalStrength
            );

            var dbRating = await _ratingRepository.GetRatingByPlayerId(player.Id);

            if (dbRating == null)
            {
                throw new KeyNotFoundException("Rating does not exist.");
            }

            dbRating.Attack = updatedRating.Attack;
            dbRating.Defense = updatedRating.Defense;
            dbRating.Tactics = updatedRating.Tactics;
            dbRating.Technique = updatedRating.Technique;
            dbRating.PhysicalStrength = updatedRating.PhysicalStrength;
            dbRating.MentalStrength = updatedRating.MentalStrength;
            dbRating.PlayerId = player.Id;

            await _ratingRepository.UpdateRatings(dbRating);

            return new RatingDto
            {
                Attack = dbRating.Attack,
                Defense = dbRating.Defense,
                Tactics = dbRating.Tactics,
                Technique = dbRating.Technique,
                PhysicalStrength = dbRating.PhysicalStrength,
                MentalStrength = dbRating.MentalStrength,
                PlayerId = player.Id
            };

        }

        private void ValidateRatingData(double attack, double defense, double tactics, double technique, double physicalStrength, double mentalStrength)
        {
            if (attack < 1 || attack > 10 ||
                defense < 1 || defense > 10 ||
                tactics < 1 || tactics > 10 ||
                technique < 1 || technique > 10 ||
                physicalStrength < 1 || physicalStrength > 10 ||
                mentalStrength < 1 || mentalStrength > 10)
            {
                throw new ArgumentException("All ratings must be between 1 and 10.");
            }
        }
    }
}
