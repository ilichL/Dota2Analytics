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
    public class IteamRepository : RepositoryBase<Iteam>, IIteamRepository
    {
        public IteamRepository(DotaContext context) : base(context) { }

        public async Task<List<Iteam>> GetMostBoughtIteamsOnHeroAsync(int count, string heroName)
        {
            return await Context.Set<Iteam>()
                .OrderBy(iteam => iteam.NumberOfPurchases)
                .Where(iteam => iteam.ItemPurchase.MatchPlayer.Hero.Name.Equals(heroName))
                .Take(count).ToListAsync();
        }
    }
}
