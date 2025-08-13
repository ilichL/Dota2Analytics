using Dota2Analytics.Data;
using Dota2Analytics.Data.Entities;
using Dota2Analytics.Data.Entities.Enums;
using Dota2Analytics.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Repositories.Implementations
{
    public class HeroRepository : RepositoryBase<Hero>, IHeroRepository
    {
        public HeroRepository(DotaContext context) : base(context) { }

        public async Task<IEnumerable<Hero>> GetHeroesByAttributeAsync(HeroAttribute attribute)//интовики, силовики
        {
            return await Context.Set<Hero>().Where(hero => hero.Attribute == attribute).ToListAsync();
        }

        public async Task<IEnumerable<Hero>> GetHeroesByRoleAsync(string role)
        {
            return await Context.Set<Hero>().Where(hero => hero.Role.Equals(role)).ToListAsync();
        }
        
        public async Task<IEnumerable<Hero>> GetHeroesByBestWinRate(int count, string role)
        {//вернет лучших(по винрейту) саппов, коров и т д
            return await Context.Set<Hero>().Where(hero => hero.Role.Equals(role))
                .OrderBy(hero => hero.HeroStats.WinRate).Take(count).ToListAsync();
        }

    }
}
