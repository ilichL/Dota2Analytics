using Dota2Analytics.Data.Abstractions;
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
        public int DierScore { get; set; }
        public int RadiantScore { get; set; }
        public string WinnerTeam { get; set; }
        public int Duration { get; set; }
        public string Mode { get; set; }//турбо, ранкед и т д
        public List<Hero> Heroes { get; set; }//герои в матче(возможно стоит убрать)
        public List<MatchPlayer> MatchPlayers { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }

    }
}
