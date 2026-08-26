using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;

namespace PlayersScouting_backend.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _dataContext;
        private readonly IStatsRepository _statsRepository;
        private readonly IRatingsRepository _ratingsRepository;

        public PlayerRepository(DataContext dataContext, IStatsRepository statsRepository, IRatingsRepository ratingsRepository)
        {
            _dataContext = dataContext;
            _statsRepository = statsRepository;
            _ratingsRepository = ratingsRepository;
        }
        public async Task CreatePlayer(Player createdPlayer)
        {
            _dataContext.Players.Add(createdPlayer);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<Player> DeletePlayer(int id)
        {
            var player = await _dataContext.Players
                /*.Include(p => p.Scouts)*/
                .FirstOrDefaultAsync(p => p.Id == id);

            /*player.Scouts.Clear();*/

            await _statsRepository.DeleteStatsByPlayerId(id);

            await _ratingsRepository.DeleteRatingByPlayerId(id);

            _dataContext.Players.Remove(player);

            await _dataContext.SaveChangesAsync();

            return player;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            var players = await _dataContext.Players
                .Include(p => p.Scouts)
                .ToListAsync();

            return players;
        }

        public async Task<Player> GetPlayer(int id)
        {
            var player = await _dataContext.Players
                .Include(p => p.Scouts)
                .FirstOrDefaultAsync(p => p.Id == id);

            return player;
        }

        public async Task<Player> GetPlayerByFullName(string fullName)
        {
            var player = await _dataContext.Players
                .FirstOrDefaultAsync(p => (p.Name + " " + p.Surname) == fullName);

            return player;
        }

        public async Task UpdatePlayer(Player updatedPlayer)
        {
            _dataContext.Players.Update(updatedPlayer);

            await _dataContext.SaveChangesAsync();
        }
    }
}
