using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Repositories
{
    public interface IStatsRepository
    {
        Task CreateStats(Stats stats);
        Task UpdateStats(Stats stats);
        Task<List<Stats>> GetAllStats();
        Task<Stats> GetStat(int id);
        Task DeleteStat(Stats stats);
        Task DeleteStatsByPlayerId(int playerId);
        Task<List<Stats>> GetStatsByPlayerId(int playerId);
    }
}
