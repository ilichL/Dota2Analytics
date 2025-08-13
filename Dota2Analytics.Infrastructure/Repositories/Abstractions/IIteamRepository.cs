using Dota2Analytics.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IIteamRepository
    {
        Task<List<Iteam>> GetMostBoughtIteamsOnHeroAsync(int count, string heroName);
    }
}
