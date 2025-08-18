using Dota2Analytics.Data.Abstractions;
using Dota2Analytics.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class Match : BaseEntity
    {
        public string SteamMatchId { get; set; }
        public DateTime MatchDate { get; set; } 
        public int DireScore { get; set; }
        public string Region { get; set; }
        public int RadiantScore { get; set; }
        public Team WinnerTeam { get; set; }
        public int Duration { get; set; } // секунды
        public string Mode { get; set; }//турбо, ранкед и т д
        public List<MatchPlayer> MatchPlayers { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }

    }
}
