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
    public class ItemPurchaseRepository : RepositoryBase<ItemPurchase>, IItemPurchaseRepository
    {
        public ItemPurchaseRepository(DotaContext context) : base(context) { }

        public async Task<ItemPurchase?> GetPlayerWithBestBuyTimeAsync(string itemName)
        {
            return await Context.Set<ItemPurchase>().Where(iteam => iteam.Iteam.Name.Equals(itemName))
                .OrderBy(iteam => iteam.PurchaseTime).FirstOrDefaultAsync();
        }

        public int? EverageUsedTime(string itemName)
        {
            var iteams = Context.Set<ItemPurchase>().Where(iteam => iteam.Iteam.Name.Equals(itemName));
            int? everageTime = null;
            foreach (var iteam in iteams)
            {
                everageTime += UsedTime(iteam) / iteams.Count();
            }

            return everageTime;
        }

        private int? UsedTime(ItemPurchase itemPurchase)
        {
            if(!itemPurchase.IsSold)
            {
                var soldtime = itemPurchase.MatchPlayer.Match.Duration;
                return soldtime - itemPurchase.PurchaseTime;
            }

            return itemPurchase.SoldTime - itemPurchase.PurchaseTime;
        }
    }
}
