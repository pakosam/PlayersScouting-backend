using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;
using System.Numerics;

namespace PlayersScouting_backend.Repositories
{
    public class ScoutRepository : IScoutRepository
    {
        private readonly DataContext _dataContext;

        public ScoutRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task CreateScout(Scout createdScout)
        {
            _dataContext.Scouts.Add(createdScout);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Scout> DeleteScout(int id)
        {
            var scout = await _dataContext.Scouts
                .Include(s => s.Players)
                .FirstOrDefaultAsync(s => s.Id == id);

            /*if (scout == null)
                return null;*/

            /*scout.Players.Clear();*/

            _dataContext.Scouts.Remove(scout);

            await _dataContext.SaveChangesAsync();

            return scout;
        }

        public async Task<List<Scout>> GetAllScouts()
        {
            var scouts = await _dataContext.Scouts
                .Include(s => s.Players)
                .ToListAsync();

            return scouts;
        }

        public async Task<Scout> GetScout(int id)
        {
            var scout = await _dataContext.Scouts
                .Include(s => s.Players)
                .FirstOrDefaultAsync(p => p.Id == id);

            return scout;
        }

        public async Task UpdateScout(Scout updatedScout)
        {
            _dataContext.Scouts.Update(updatedScout);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _dataContext.Scouts
                .AnyAsync(e => e.Email == email);
        }

        public async Task<Scout> GetUserByEmailAsync(string email)
        {
            return await _dataContext.Scouts
                .FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
