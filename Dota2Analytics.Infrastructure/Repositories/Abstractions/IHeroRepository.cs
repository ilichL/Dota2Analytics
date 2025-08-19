using Dota2Analytics.Data.Entities;
using Dota2Analytics.Data.Entities.Enums;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Hero>> GetHeroesByAttributeAsync(HeroAttribute attribute);
        Task<IEnumerable<Hero>> GetHeroesByRoleAsync(string role);
        Task<IEnumerable<Hero>> GetHeroesByBestWinRateASync(int count, string role);
        Task<Hero> GetHeroByNameAsync(string heroName);
    }
}
