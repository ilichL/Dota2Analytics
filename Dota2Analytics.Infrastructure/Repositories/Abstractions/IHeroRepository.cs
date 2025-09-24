using Dota2Analytics.Data.Entities;
<<<<<<< HEAD
using Dota2Analytics.Data.Entities.Enums;
using System.Threading.Tasks;
=======
using Dota2Analytics.Models.Enums;
>>>>>>> new-version

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
