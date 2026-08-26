using PlayersScouting_backend.DTOs;

namespace PlayersScouting_backend.Services
{
    public interface IStatsService
    {
        Task<StatDto> CreateStats(CreateStatDto createdStat);
        Task<StatDto> UpdateStats(UpdateStatDto updatedStat);
        Task<List<StatDto>> GetAllStats();
        Task<StatDto> GetStat(int id);
        Task<StatDto> DeleteStat(int id);
        Task<List<StatDto>> GetStatsByPlayerId(int playerId);
    }
}
