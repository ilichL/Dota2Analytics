using Dota2Analytics.Data.Abstractions;
<<<<<<< HEAD
using Dota2Analytics.Data.Entities.Enums;
=======
using Dota2Analytics.Models.Enums;
>>>>>>> new-version

namespace Dota2Analytics.Data.Entities
{
    public class MatchEvent : BaseEntity
    {
        public EventType EventType { get; set; }//саппорт, рошан, сломанный тавер и т д
        public RuneType? RuneType { get; set; }
        public string? Data { get; set; }
<<<<<<< HEAD
        public int MatchId { get; set; }
=======
        public Guid MatchId { get; set; }
>>>>>>> new-version
        public Match Match { get; set; }
        public List<Player>? Players { get; set; }
        public List<ItemPurchase>? ItemPurchases { get; set; }
    }
}
