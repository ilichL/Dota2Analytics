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
    public class MatchEventRepository : RepositoryBase<MatchEvent>, IMatchEventRepository
    {
        public MatchEventRepository(DotaContext context) : base(context) { }

        public async Task<Match?> GetMatchBySteanIdAsync(string steamId)
        {
            return await _context.Set<Match>().Where(match => match.SteamMatchId.Equals(steamId)).FirstOrDefaultAsync();
        }

        public async Task<List<Match>> GetMatchesWithEventsAsync(List<MatchEvent> matchEvents)
        {
            return await _context.Set<Match>()
                .Where(match => matchEvents.All(me => match.MatchEvents.Contains(me)))
                .ToListAsync();
        }
    }
}
