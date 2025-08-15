using Dota2Analytics.Data.Entities;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IMatchEventRepository
    {
        Task<Match?> GetMatchBySteanIdAsync(string steamId);
        Task<List<Match>> GetMatchesWithEventsAsync(List<MatchEvent> matchEvents);
    }
}
