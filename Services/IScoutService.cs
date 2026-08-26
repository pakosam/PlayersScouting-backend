using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;

namespace PlayersScouting_backend.Services
{
    public interface IScoutService
    {
        Task<ScoutDto> CreateScout(CreateScoutDto createdScout);
        Task<ScoutDto> UpdateScout(UpdateScoutDto updatedScout);
        Task<List<ScoutDto>> GetAllScouts();
        Task<ScoutDto> GetScout(int id);
        Task<ScoutDto> DeleteScout(int id);
    }
}
