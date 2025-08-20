using Dota2Analytics.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Get();
        Task<TEntity> GetByIdAsync(int id);
        Task RemoveAsync(int id);
        Task AddAsync(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
    }
}
