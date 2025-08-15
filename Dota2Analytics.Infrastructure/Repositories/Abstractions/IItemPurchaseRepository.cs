using Dota2Analytics.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Repositories.Abstractions
{
    public interface IItemPurchaseRepository
    {
        Task<ItemPurchase?> GetPlayerWithBestBuyTimeAsync(string itemName);
        int? EverageUsedTime(string itemName);
    }
}
