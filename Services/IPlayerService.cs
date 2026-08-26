using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Services
{
    public interface IPlayerService
    {
        Task<PlayerDto> CreatePlayer(CreatePlayerDto createdPlayer);
        Task<PlayerDto> UpdatePlayer(UpdatePlayerDto updatedPlayer);
        Task<List<PlayerDto>> GetAllPlayers();
        Task<PlayerDto> GetPlayer(int id);
        Task<PlayerDto> DeletePlayer(int id);
        Task<Player> GetPlayerByFullName(string fullName);
    }
}
