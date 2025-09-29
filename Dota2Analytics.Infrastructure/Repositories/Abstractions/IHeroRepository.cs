using Dota2Analytics.Data.Entities;
using Dota2Analytics.Data.Entities.Enums;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IHeroRepository
    {
        Task AddRange(IEnumerable<Hero> entities);
        Task<IEnumerable<Hero>> GetHeroesByAttributeAsync(HeroAttribute attribute);
        Task<IEnumerable<Hero>> GetHeroesByRoleAsync(string role);
        Task<IEnumerable<Hero>> GetHeroesByBestWinRateASync(int count, string role);
        Task<Hero> GetHeroByNameAsync(string heroName);
        Task<List<Hero>> GetHeroesByOpenDotaIds(List<int?> ids);
    }
}
