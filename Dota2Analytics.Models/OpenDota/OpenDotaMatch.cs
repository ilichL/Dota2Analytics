using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Models.OpenDota
{
    public class OpenDotaMatch
    {
        public string SteamMatchId { get; set; }
        public string WinnerTeam {  get; set; }
        public string Mode { get; set; }
        public int Duration { get; set; }
    }
}
