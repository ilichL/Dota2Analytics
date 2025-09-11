using Dota2Analytics.Data.Abstractions;
using Dota2Analytics.Data.Entities.Enums;

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
        public int IteamId { get; set; }
        public Iteam Iteam { get; set; }
        public int MatchPlayerId { get; set; }
        public MatchPlayer MatchPlayer { get; set; }

    }
}
