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
    public class MatchRepository : RepositoryBase<Match> , IMatchRepository
    {
        public MatchRepository(DotaContext context) : base(context){ }

        public async Task<List<Match>> GetMatchesByModeAsync (string mode)
        {
            return await _context.Set<Match>().Where(match => match.Mode.Equals(mode)).ToListAsync();
        }

        public async Task<List<Match>> GetMathcesByUserNickNameAsync (string nickName)
        {//все матчи одного игрока
            return await _context.Set<Match>().Where(match => match.MatchPlayers
            .Any(matchPlayer => matchPlayer.Player.NickName.Equals(nickName)))
            .OrderBy(match => match.MatchDate).ToListAsync();
        }

        public async Task<List<Match>> GetMathcesByUserNickNameWithModeAsync(string nickName, string mode)
        {//матчи одного героя в одном режиме
            return  (await GetMathcesByUserNickNameAsync(nickName)).Where(match => match.Mode.Equals(mode)).ToList();
        }

        public async Task UpdateRange(List<Match> matches)
        {
            _context.UpdateRange(matches);
            await _context.SaveChangesAsync();
        }
    }
}
