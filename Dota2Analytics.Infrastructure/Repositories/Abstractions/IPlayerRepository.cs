using Dota2Analytics.Data.Entities;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IPlayerRepository
    {
        Task<Player?> GetPlayerBySteamIdAsync(int SteamId);
        Task<Player?> GetPlayerByNickNameAsync(string nickName);
        Task<IEnumerable<Player>> GetTopRankedPlayersAsync(int count);
        Task<IEnumerable<Player>> GetTopWinRatePlayersAsync(int count);
    }
}
