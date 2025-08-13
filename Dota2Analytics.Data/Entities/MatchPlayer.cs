using Dota2Analytics.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class MatchPlayer : BaseEntity
    {
        public int? Kills { get; set; }
        public int? Death {  get; set; }
        public int? Assists { get; set; }
        public int? BuildingDamage { get; set; }
        public int? HeroDamage { get; set; }
        public int? DamageReceivedRaw { get; set; }
        public int? DamageReceivedReduced {  get; set; }
        public int Pick { get; set; }//какой по счету пикнули героя
        public int? SupportGoldSpent { get; set; }
        public int? CampsStacked { get; set; }//стакнуто кемпов
        public int NetWorth { get; set; }//общая ценность 
        public int? CreepsLastHit { get; set; }//добито крипов
        public int? CreepsDenies { get; set; }
        public int? BountyRunes { get; set; }
        public int Gpm {  get; set; }//gold per minute
        public int Xpm { get; set; }// Experience per minute
        public int? OutpostCaptured { get; set; }//захваченные аванпосты
        public int? Heal { get; set; }//хил тимейтам 
        public int? PlayerLevel { get; set; }
        public int? MatchId { get; set; }
        public Match? Match { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        public List<string>? SupportContribution { get; set; }//варды, смоки 
        public List<ItemPurchase>? ItemPurchase { get; set; }
    }
}
