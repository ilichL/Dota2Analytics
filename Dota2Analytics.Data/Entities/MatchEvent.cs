using Dota2Analytics.Data.Abstractions;
using Dota2Analytics.Data.Entities.Enums;

namespace Dota2Analytics.Data.Entities
{
    public class MatchEvent : BaseEntity
    {
        public EventType EventType { get; set; }//саппорт, рошан, сломанный тавер и т д
        public RuneType? RuneType { get; set; }
        public string? Data { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public List<Player>? Players { get; set; }
        public List<ItemPurchase>? ItemPurchases { get; set; }
    }
}
