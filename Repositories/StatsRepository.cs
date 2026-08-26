using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly DataContext _dataContext;
        public StatsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task CreateStats(Stats stats)
        {
            _dataContext.Stats.Add(stats);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteStat(Stats stats)
        {
            _dataContext.Stats.Remove(stats);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteStatsByPlayerId(int playerId)
        {
            var stats = await _dataContext.Stats
                .Where(st => st.PlayerId == playerId)
                .ToListAsync();

            _dataContext.Stats.RemoveRange(stats);
        }

        public async Task<List<Stats>> GetAllStats()
        {
            var stats = await _dataContext.Stats.ToListAsync();

            return stats;
        }

        public async Task<Stats> GetStat(int id)
        {
            var stat = await _dataContext.Stats.FindAsync(id);

            return stat;
        }

        public async Task<List<Stats>> GetStatsByPlayerId(int playerId)
        {
            var stat = await _dataContext.Stats
                .Where(s => s.PlayerId == playerId)
                .ToListAsync();

            return stat;

        }

        public async Task UpdateStats(Stats stats)
        {
            _dataContext.Stats.Update(stats);

            await _dataContext.SaveChangesAsync();
        }
    }
}
