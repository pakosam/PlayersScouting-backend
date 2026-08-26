using Microsoft.AspNetCore.Mvc;
using PlayersScouting_backend.DTOs;
using PlayersScouting_backend.Entities;
using PlayersScouting_backend.Services;

namespace PlayersScouting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoutsController : ControllerBase
    {
        private readonly IScoutService _scoutService;
        public ScoutsController(IScoutService scoutService)
        {
            _scoutService = scoutService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Scout>>> GetAllScouts()
        {
            var scouts = await _scoutService.GetAllScouts();

            return Ok(scouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScoutDto>> GetSingleScout(int id)
        {
            var scout = await _scoutService.GetScout(id);

            return scout;
        }

        [HttpPost]
        public async Task<ActionResult<ScoutDto>> AddScout(CreateScoutDto createScoutDto)
        {
            var scout = await _scoutService.CreateScout(createScoutDto);

            return Ok(scout);
        }

        [HttpPut]
        public async Task<ActionResult<Scout>> UpdateScout(UpdateScoutDto updateScoutDto)
        {
            var scout = await _scoutService.UpdateScout(updateScoutDto);

            return Ok(scout);
        }

        [HttpDelete]
        public async Task<ActionResult<Scout>> DeleteScout(int id)
        {
            var scout = await _scoutService.DeleteScout(id);

            return Ok(scout);
        }
    }
}
