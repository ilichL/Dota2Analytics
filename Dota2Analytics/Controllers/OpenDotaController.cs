using Dota2Analytics.Infrastructure.Services.Abstractions;
using Dota2Analytics.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dota2Analytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenDotaController : ControllerBase
    {
        private readonly IOpenDotaAPIService _openDotaAPIService;
        private readonly ILogger<OpenDotaController> _logger;

        public OpenDotaController(IOpenDotaAPIService openDotaAPIService, ILogger<OpenDotaController> logger)
        {
            _openDotaAPIService = openDotaAPIService;
            _logger = logger;
        }

        [HttpGet("ParseHeroes")]
        public async Task<IActionResult> ParseHeroes()
        {
            var result = await _openDotaAPIService.ParseHeroesAsync();

            if (result is not null)
            {
                return Ok(result);
            }

            _logger.LogError("BadRequest while parsing heroes in OpenDotaController");
            return BadRequest();
        }

        [HttpGet("GetPlayerBySteamId")]
        public async Task<IActionResult> GetPlayerBySteamId(string steamAccountId)
        {
            var result = await _openDotaAPIService.UpdtaePlayerAsync(steamAccountId);

            if (result is not null)
            {
                return Ok(result);
            }

            _logger.LogError($"BadRequest while Updating player with steam account id = {steamAccountId} in OpenDotaController");
            return BadRequest();
        }

        [HttpGet("GetMatchesBySteamId")]
        public async Task<IActionResult> GetMatchesBySteamId(string steamAccountId)
        {
            var result = await _openDotaAPIService.GetMatchesByUserSteamIdAsync(steamAccountId);

            if (result is not null)
            {
                return Ok(result);
            }

            _logger.LogError($"BadRequest while parsing matches with steam account id = {steamAccountId} in OpenDotaController");
            return BadRequest();
        }

    }
}
