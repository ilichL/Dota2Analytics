using Dota2Analytics.Data.Abstractions;
using Dota2Analytics.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class ItemPurchase : BaseEntity
    {//статистика таймингов предметов
        public DateTime PurchaseTime { get; set; }//время покупки в матче(тайминг появления, пример бф к 30й)
        public bool IsSold { get; set; }//продан ли 
        public DateTime? SoldTime { get; set; }//время продажи в матче
        public bool? WasEaten { get; set; }//аганим(не будет поля, если предмет не подразумевает такой возможности)
        public DateTime? EatTime { get; set; }
        public ItemSourceType Source {  get; set; }//как получен(куплен, выбит)
        public int IteamId { get; set; }
        public Iteam Iteam { get; set; }
        public int MatchPlayerId { get; set; }
        public MatchPlayer MatchPlayer { get; set; }

    }
}
