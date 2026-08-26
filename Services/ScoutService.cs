using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Repositories;

namespace PlayersScouting_backend.Services
{
    public class ScoutService : IScoutService
    {
        private readonly IScoutRepository _scoutRepository;
        private readonly IPlayerService _playerService;

        public ScoutService (IScoutRepository scoutRepository, IPlayerService playerService)
        {
            _scoutRepository = scoutRepository;
            _playerService = playerService;
        }

        public async Task<ScoutDto> CreateScout(CreateScoutDto scoutDto)
        {
            var createdScout = new Scout
            {
                Name = scoutDto.Name,
                Surname = scoutDto.Surname,
                Birthdate = scoutDto.Birthdate,
                Birthplace = scoutDto.Birthplace,
                Email = scoutDto.Email,
                Password = scoutDto.Password
            };

            foreach (var fullName in scoutDto.PlayerFullNames)
            {
                var player = await _playerService.GetPlayerByFullName(fullName);

                createdScout.Players.Add(player);
            }

            await _scoutRepository.CreateScout(createdScout);

            return new ScoutDto
            {
                Id = createdScout.Id,
                Name = createdScout.Name,
                Surname = createdScout.Surname,
                Birthdate = createdScout.Birthdate,
                Birthplace = createdScout.Birthplace,
                Email = createdScout.Email,
                PlayersId = createdScout.Players.Select(p => p.Id).ToList()
            };
        }

        public async Task<ScoutDto> DeleteScout(int id)
        {
            var scout = await _scoutRepository.DeleteScout(id);
                       
            if (scout == null)
            {
                throw new ArgumentException($"Scout with ID {id} does not exist.");
            }

            return new ScoutDto
            {
                Id = scout.Id,
                Name = scout.Name,
                Surname = scout.Surname,
                Birthdate = scout.Birthdate,
                Birthplace = scout.Birthplace,
                Email = scout.Email,
                PlayersId = scout.Players.Select(p => p.Id).ToList()
            };
        }

        public async Task<List<ScoutDto>> GetAllScouts()
        {
            var scouts = await _scoutRepository.GetAllScouts();

            if (scouts == null)
            {
                throw new Exception($"Scouts don't exist.");
            }

            return scouts.Select(s => new ScoutDto
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                Birthdate = s.Birthdate,
                Birthplace = s.Birthplace,
                Email = s.Email,
                PlayersId = s.Players.Select(p => p.Id).ToList()
            }).ToList();
        }

        public async Task<ScoutDto> GetScout(int id)
        {
            var scout = await _scoutRepository.GetScout(id);

            if (scout == null)
            {
                throw new KeyNotFoundException($"Scout with ID {id} does not exist.");
            }

            return new ScoutDto
            {
                Id = scout.Id,
                Name = scout.Name,
                Surname = scout.Surname,
                Birthdate = scout.Birthdate,
                Birthplace = scout.Birthplace,
                Email = scout.Email,
                PlayersId = scout.Players.Select(s => s.Id).ToList()
            };
        }

        public async Task<ScoutDto> UpdateScout(UpdateScoutDto updatedScout)
        {
            var dbScout = await _scoutRepository.GetScout(updatedScout.Id);

            if (dbScout == null)
                throw new ArgumentException($"Scout with ID {updatedScout.Id} does not exist.");

            dbScout.Name = updatedScout.Name;
            dbScout.Surname = updatedScout.Surname;
            dbScout.Birthdate = updatedScout.Birthdate;
            dbScout.Birthplace = updatedScout.Birthplace;
            dbScout.Email = updatedScout.Email;
            dbScout.Password = updatedScout.Password;

            foreach (var fullName in updatedScout.PlayerFullNames)
            {
                var player = await _playerService.GetPlayerByFullName(fullName);

                if (player != null && !dbScout.Players.Any(p => p.Id == player.Id))
                {
                    dbScout.Players.Add(player);
                }
            }

            await _scoutRepository.UpdateScout(dbScout);

            return new ScoutDto
            {
                Id = dbScout.Id,
                Name = dbScout.Name,
                Surname = dbScout.Surname,
                Birthdate = dbScout.Birthdate,
                Birthplace = dbScout.Birthplace,
                Email = dbScout.Email,
                PlayersId = dbScout.Players.Select(p => p.Id).ToList()
            };
        }
    }
}
