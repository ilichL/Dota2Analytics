using Dota2Analytics.Data;
using Dota2Analytics.Data.Entities;
<<<<<<< HEAD
using Dota2Analytics.Data.Entities.Enums;
using Dota2Analytics.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
=======
using Dota2Analytics.Infrastructure.Repositories.Abstractions;
using Dota2Analytics.Models.Enums;
using Microsoft.EntityFrameworkCore;
>>>>>>> new-version

namespace Dota2Analytics.Infrastructure.Repositories.Implementations
{
    public class HeroRepository : RepositoryBase<Hero>, IHeroRepository
    {
        public HeroRepository(DotaContext context) : base(context) { }

        public async Task<IEnumerable<Hero>> GetHeroesByAttributeAsync(HeroAttribute attribute)//интовики, силовики
        {
            return await _context.Set<Hero>().Where(hero => hero.Attribute == attribute).ToListAsync();
        }

        public async Task<IEnumerable<Hero>> GetHeroesByRoleAsync(string role)
        {
            var heroRole = SwitchRole(role);
            return await _context.Set<Hero>().Where(hero => hero.Roles.Contains(heroRole)).ToListAsync();
        }
        
        public async Task<IEnumerable<Hero>> GetHeroesByBestWinRateASync(int count, string role)
        {//вернет лучших(по винрейту) саппов, коров и т д
            var heroRole = SwitchRole(role);
            return await _context.Set<Hero>().Where(hero => hero.Roles.Contains(heroRole))
                .OrderBy(hero => hero.HeroStats.WinRate).Take(count).ToListAsync();
        }

        public async Task<Hero> GetHeroByNameAsync(string heroName)
        {
            return await _context.Set<Hero>().Where(hero => hero.Name.Equals(heroName)).FirstOrDefaultAsync();
        }

        public async Task<List<Hero>> GetHeroesByOpenDotaIds(List<int?> ids)
        {
            return await _context.Set<Hero>().Where(hero => ids.Contains(hero.OpenDotaId)).AsNoTracking().ToListAsync();
        }

        private HeroRole? SwitchRole(string role)
        {
            switch (role)
            {
                case "HardSupport":
                {
                    return HeroRole.HardSupport;
                }
                case "Support5":
                {
                    return HeroRole.Support5;
                }
                case "OffLane":
                {
                    return HeroRole.Support5;
                }
                case "MidLane":
                {
                    return HeroRole.Support5;
                    }
                case "SafeLane":
                {
                    return HeroRole.Support5;
                }
                case "Support":
                {
                    return HeroRole.Support;
                }
                case "Carry":
                {
                    return HeroRole.Carry;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
