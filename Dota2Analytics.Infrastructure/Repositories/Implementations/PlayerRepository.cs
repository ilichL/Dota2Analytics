using Dota2Analytics.Data;
using Dota2Analytics.Data.Entities;
using Dota2Analytics.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Repositories.Implementations
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(DotaContext context) : base(context) { }

        public async Task<Player?> GetPlayerBySteamIdAsync(int SteamId)
        {
            return await Context.Set<Player>().FirstOrDefaultAsync(player => player.SteamAccountId == SteamId);
        }

        public async Task<Player?> GetPlayerByNickNameAsync(string nickName)
        {
            return await Context.Set<Player>().FirstOrDefaultAsync(player => player.NickName.Equals(nickName));
        }

        public async Task<IEnumerable<Player>> GetTopRankedPlayersAsync(int count)
        {
            return await Context.Set<Player>().OrderBy(player => player.Rank).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetTopWinRatePlayersAsync(int count)
        {
            return await Context.Set<Player>().OrderBy(player => player.WinRate).Take(count).ToListAsync();
        }
    }
}
