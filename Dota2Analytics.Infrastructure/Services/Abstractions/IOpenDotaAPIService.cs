using Dota2Analytics.Models.OpenDota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Services.Abstractions
{
    public interface IOpenDotaAPIService
    {
        Task<OpenDotaPlayerDto>? UpdtaePlayerAsync(string steamAccountId);
        Task<List<OpenDotaHeroDto>>? ParseHeroesAsync();
        Task<List<OpenDotaMatch>>? GetMatchesByUserSteamIdAsync(string steamAccountId);
        Task GetMatсhAsync(string matchId);
    }
}
