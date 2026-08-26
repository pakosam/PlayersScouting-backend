using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Repositories;

namespace PlayersScouting_backend.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public async Task<PlayerDto> CreatePlayer(CreatePlayerDto playerDto)
        {
            ValidateAddedPlayerData(playerDto);

            var createdPlayer = new Player
            {
                Name = playerDto.Name,
                Surname = playerDto.Surname,
                Birthdate = playerDto.Birthdate,
                Birthplace = playerDto.Birthplace,
                Age = DateOnly.FromDateTime(DateTime.Today).Year - playerDto.Birthdate.Year,
                Height = playerDto.Height,
                Foot = playerDto.Foot,
                ShirtNumber = playerDto.ShirtNumber,
                Positions = playerDto.Positions,
                Club = playerDto.Club
            };

            await _playerRepository.CreatePlayer(createdPlayer);

            return new PlayerDto
            {
                Id = createdPlayer.Id,
                Name = createdPlayer.Name,
                Surname = createdPlayer.Surname,
                Birthdate = createdPlayer.Birthdate,
                Birthplace = createdPlayer.Birthplace,
                Height = createdPlayer.Height,
                Foot = createdPlayer.Foot,
                ShirtNumber = createdPlayer.ShirtNumber,
                Positions = createdPlayer.Positions,
                Club = createdPlayer.Club
            };
        }

        private void ValidatePlayerData(string name, string surname, DateOnly birthdate, string birthplace, int height, string foot, int shirtNumber, string positions, string club)
        {
            var calculatedAge = DateOnly.FromDateTime(DateTime.Today).Year - birthdate.Year;

            if (birthdate > DateOnly.FromDateTime(DateTime.Today).AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (name.Length < 3)
                throw new ArgumentException("Name must be at least 3 characters long.");
            if (surname.Length < 3)
                throw new ArgumentException("Surname must be at least 3 characters long.");
            if (calculatedAge < 18 || calculatedAge > 45)
                throw new ArgumentException("Age must be higher than 18 or lower than 45.");
            if (height > 230 || height < 150)
                throw new ArgumentException("Wrong input.");
            if (foot.ToLower() != "left" && foot.ToLower() != "right")
                throw new ArgumentException("Foot must be 'left' or 'right'.");
            if (shirtNumber < 1 || shirtNumber > 99)
                throw new ArgumentException("Shirt number must be between 1 and 99.");
        }

        public async Task<PlayerDto> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            var dbPlayer = await _playerRepository.GetPlayer(updatedPlayer.Id);

            if (dbPlayer == null)
                throw new ArgumentException($"Player with ID {updatedPlayer.Id} does not exist.");

            ValidateUpdatedPlayerData(updatedPlayer);

            dbPlayer.Name = updatedPlayer.Name;
            dbPlayer.Surname = updatedPlayer.Surname;
            dbPlayer.Birthdate = updatedPlayer.Birthdate;
            dbPlayer.Birthplace = updatedPlayer.Birthplace;
            dbPlayer.Age = DateOnly.FromDateTime(DateTime.Today).Year - updatedPlayer.Birthdate.Year;
            dbPlayer.Height = updatedPlayer.Height;
            dbPlayer.Foot = updatedPlayer.Foot;
            dbPlayer.ShirtNumber = updatedPlayer.ShirtNumber;
            dbPlayer.Positions = updatedPlayer.Positions;
            dbPlayer.Club = updatedPlayer.Club;


            await _playerRepository.UpdatePlayer(dbPlayer);

            return new PlayerDto
            {
                Id = dbPlayer.Id,
                Name = dbPlayer.Name,
                Surname = dbPlayer.Surname,
                Birthdate = dbPlayer.Birthdate,
                Birthplace = dbPlayer.Birthplace,
                Height = dbPlayer.Height,
                Foot = dbPlayer.Foot,
                ShirtNumber = dbPlayer.ShirtNumber,
                Positions = dbPlayer.Positions,
                Club = dbPlayer.Club
            };
        }

        public async Task<List<PlayerDto>> GetAllPlayers()
        {
            var players = await _playerRepository.GetAllPlayers();

            if (players == null)
            {
                throw new Exception($"Players don't exist.");
            }

            return players.Select(p => new PlayerDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Birthdate = p.Birthdate,
                Birthplace = p.Birthplace,
                Height = p.Height,
                Foot = p.Foot,
                ShirtNumber = p.ShirtNumber,
                Positions = p.Positions,
                Club = p.Club,
                ScoutsId = p.Scouts.Select(p => p.Id).ToList()
            }).ToList();
        }

        public async Task<PlayerDto> GetPlayer(int id)
        {
            var player = await _playerRepository.GetPlayer(id);

            if (player == null)
            {
                throw new KeyNotFoundException($"Player with ID {id} does not exist.");
            }

            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Surname = player.Surname,
                Birthdate = player.Birthdate,
                Birthplace = player.Birthplace,
                Height = player.Height,
                Foot = player.Foot,
                ShirtNumber = player.ShirtNumber,
                Positions = player.Positions,
                Club = player.Club,
                ScoutsId = player.Scouts.Select(p => p.Id).ToList()
            };
        }

        public async Task<PlayerDto> DeletePlayer(int id)
        {
            var player = await _playerRepository.DeletePlayer(id);

            if (player == null)
            {
                throw new ArgumentException($"Player with ID {id} does not exist.");
            }

            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Surname = player.Surname,
                Birthdate = player.Birthdate,
                Birthplace = player.Birthplace,
                Height = player.Height,
                Foot = player.Foot,
                ShirtNumber = player.ShirtNumber,
                Positions = player.Positions,
                Club = player.Club
            };
        }

        private void ValidateAddedPlayerData(CreatePlayerDto player)
        {
            ValidatePlayerData(player.Name, player.Surname, player.Birthdate, player.Birthplace, player.Height, player.Foot, player.ShirtNumber, player.Positions, player.Club);
        }

        private void ValidateUpdatedPlayerData(UpdatePlayerDto player)
        {
            ValidatePlayerData(player.Name, player.Surname, player.Birthdate, player.Birthplace, player.Height, player.Foot, player.ShirtNumber, player.Positions, player.Club);
        }

        public async Task<Player> GetPlayerByFullName(string fullName)
        {
            var player = await _playerRepository.GetPlayerByFullName(fullName);

            if (player == null)
            {
                throw new ArgumentException($"Player {fullName} does not exist.");
            }

            return player;
        }
    }
}
