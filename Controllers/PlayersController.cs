using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Persistence;
using PlayersScouting_backend.Services;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAllPlayers()
        {
            var players = await _playerService.GetAllPlayers();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetSinglePlayer(int id)
        {
            var player = await _playerService.GetPlayer(id);

            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer(CreatePlayerDto createPlayerDto)
        {
            var player = await _playerService.CreatePlayer(createPlayerDto);

            return Ok(player);
        }

        [HttpPut]
        public async Task<ActionResult<Player>> UpdatePlayer(UpdatePlayerDto updatedPlayerDto)
        {
            var player = await _playerService.UpdatePlayer(updatedPlayerDto);

            return Ok(player);
        }

        [HttpDelete]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            var player = await _playerService.DeletePlayer(id);

            return Ok(player);
        }
    }
}