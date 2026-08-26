using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Repositories
{
    public interface IScoutRepository
    {
        Task CreateScout(Scout scout);
        Task UpdateScout(Scout scout);
        Task<List<Scout>> GetAllScouts();
        Task<Scout> GetScout(int id);
        Task<Scout> DeleteScout(int id);
        Task<bool> UserExistsAsync(string email);
        Task<Scout> GetUserByEmailAsync(string email);
    }
}
