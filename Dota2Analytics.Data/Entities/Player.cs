using Dota2Analytics.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public int? SteamAccountId { get; set; }
        public int WinRate { get; set; }
        public int Rank { get; set; }
        public int? MatchEventId { get; set; }
        public MatchEvent? MatchEvent { get; set; }
        public MatchPlayer? MatchPlayer { get; set; }
    }
}
