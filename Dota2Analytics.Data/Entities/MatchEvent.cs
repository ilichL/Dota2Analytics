using Dota2Analytics.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class MatchEvent : BaseEntity
    {
        public string EventType { get; set; }//саппорт, рошан, сломанный тавер и т д
        public string? Data { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public List<Player>? Players { get; set; }
    }
}
