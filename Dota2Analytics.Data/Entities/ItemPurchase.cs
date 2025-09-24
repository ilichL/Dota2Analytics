using Dota2Analytics.Data.Abstractions;
<<<<<<< HEAD
using Dota2Analytics.Data.Entities.Enums;
=======
using Dota2Analytics.Models.Enums;
>>>>>>> new-version

namespace Dota2Analytics.Data.Entities
{
    public class ItemPurchase : BaseEntity
    {//статистика таймингов предметов
        public int PurchaseTime { get; set; }//время покупки в матче(тайминг появления, пример бф к 30й)
        public bool IsSold { get; set; }//продан ли 
        public int? SoldTime { get; set; }//время продажи в матче
        public bool? WasEaten { get; set; }//аганим(не будет поля, если предмет не подразумевает такой возможности)
        public int? EatTime { get; set; }
        public ItemSourceType Source { get; set; }//как получен(куплен, выбит)
<<<<<<< HEAD
        public int IteamId { get; set; }
        public Iteam Iteam { get; set; }
        public int MatchPlayerId { get; set; }
=======
        public Guid IteamId { get; set; }
        public Iteam Iteam { get; set; }
        public Guid MatchPlayerId { get; set; }
>>>>>>> new-version
        public MatchPlayer MatchPlayer { get; set; }

    }
}
