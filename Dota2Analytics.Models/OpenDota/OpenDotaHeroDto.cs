using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Models.OpenDota
{
    public class OpenDotaHeroDto
    {
        public int? OpenDotaId { get; set; }
        public string Name { get; set; }
        public string Attribute { get; set; }
        public List<string> Roles { get; set; }
        public string AttackType { get; set; }
        public int DayVision { get; set; }
        public int NightVision { get; set; }
    }
}
