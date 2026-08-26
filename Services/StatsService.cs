using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Repositories;

namespace PlayersScouting_backend.Services
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statRepository;
        private readonly IPlayerService _playerService;

        public StatsService(IStatsRepository statRepository, IPlayerService playerService)
        {
            _statRepository = statRepository;
            _playerService = playerService;
        }
        public async Task<StatDto> CreateStats(CreateStatDto statDto)
        {
            var player = await _playerService.GetPlayerByFullName(statDto.FullName);

            if (player == null)
            {
                throw new KeyNotFoundException($"Player with name {statDto.FullName} does not exist.");
            }

            var createdStat = new Stats
            {
                Season = statDto.Season,
                Club = statDto.Club,
                MatchesPlayed = statDto.MatchesPlayed,
                Goals = statDto.Goals,
                Assists = statDto.Assists,
                PlayerId = player.Id
            };

            await _statRepository.CreateStats(createdStat);

            return new StatDto
            {
                Season = createdStat.Season,
                Club = createdStat.Club,
                MatchesPlayed = createdStat.MatchesPlayed,
                Goals = createdStat.Goals,
                Assists = createdStat.Assists,
            };
        }

        public async Task<StatDto> DeleteStat(int id)
        {
            var stat = await _statRepository.GetStat(id);

            if (stat == null)
                throw new KeyNotFoundException("Stat not found.");

            await _statRepository.DeleteStat(stat);

            return new StatDto
            {
                Season = stat.Season,
                Club = stat.Club,
                MatchesPlayed = stat.MatchesPlayed,
                Goals = stat.Goals,
                Assists = stat.Assists,
            };
        }

        public async Task<List<StatDto>> GetAllStats()
        {
            var stats = await _statRepository.GetAllStats();

            if (stats == null)
            {
                throw new Exception($"Stats don't exist.");
            }

            return stats.Select(s => new StatDto
            {
                Season = s.Season,
                Club = s.Club,
                MatchesPlayed = s.MatchesPlayed,
                Goals = s.Goals,
                Assists = s.Assists,
                PlayerId = s.PlayerId
            }).ToList();
        }

        public async Task<StatDto> GetStat(int id)
        {
            var stat = await _statRepository.GetStat(id);

            if (stat == null)
            {
                throw new KeyNotFoundException($"Stat with ID {id} does not exist.");
            }

            return new StatDto
            {
                Id = stat.Id,
                Season = stat.Season,
                Club = stat.Club,
                MatchesPlayed = stat.MatchesPlayed,
                Goals = stat.Goals,
                Assists = stat.Assists,
                PlayerId = stat.PlayerId
            };
        }

        public async Task<List<StatDto>> GetStatsByPlayerId(int playerId)
        {
            var stats = await _statRepository.GetStatsByPlayerId(playerId);

            if (stats == null)
            {
                throw new Exception($"Stats don't exist.");
            }

            return stats.Select(s => new StatDto
            {
                Id = s.Id,
                Season = s.Season,
                Club = s.Club,
                MatchesPlayed = s.MatchesPlayed,
                Goals = s.Goals,
                Assists = s.Assists,
                PlayerId = s.PlayerId
            }).ToList();
        }

        public async Task<StatDto> UpdateStats(UpdateStatDto updatedStat)
        {
            /*var player = await _playerService.GetPlayerByFullName(updatedStat.FullName);

            if (player == null)
            {
                throw new KeyNotFoundException($"Player with name {updatedStat.FullName} does not exist.");
            }
            */

            var dbStat = await _statRepository.GetStat(updatedStat.Id);

            if (dbStat == null)
            {
                throw new KeyNotFoundException("Stats do not exist");
            }

            dbStat.Season = updatedStat.Season;
            dbStat.Club = updatedStat.Club;
            dbStat.MatchesPlayed = updatedStat.MatchesPlayed;
            dbStat.Goals = updatedStat.Goals;
            dbStat.Assists = updatedStat.Assists;
            // dbStat.PlayerId = player.Id;

            await _statRepository.UpdateStats(dbStat);

            return new StatDto
            {
                Id = dbStat.Id,
                Season = dbStat.Season,
                Club = dbStat.Club,
                MatchesPlayed = dbStat.MatchesPlayed,
                Goals = dbStat.Goals,
                Assists = dbStat.Assists,
                PlayerId = dbStat.PlayerId
            };
        }
    }
}
