using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Repositories
{
    public interface IPlayerRepository
    {
        Task CreatePlayer(Player player);
        Task UpdatePlayer(Player player);
        Task<List<Player>> GetAllPlayers();
        Task<Player> GetPlayer(int id);
        Task<Player> DeletePlayer(int id);
        Task<Player> GetPlayerByFullName(string fullName);
    }
}
