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
        public List<Hero> Heroes { get; set; }//герои в матче
        public List<Player> Players { get; set; }
        public int DierScore { get; set; }
        public int RadiantScore { get; set; }
        public string WinnerTeam { get; set; }
    }
}
