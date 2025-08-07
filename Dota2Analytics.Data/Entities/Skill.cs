using Dota2Analytics.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public string? Type { get; set; }//направленная на героя,пасивная, ненаправленная, направленная на точку
        public string? DamageType { get; set; }//магический, физический, чистый
        public int? Range { get; set; }
        public string? Affects { get; set; }//на вражеских героев, на союзных героев
        public bool? Dispellable { get; set; }
        public bool? PiercesDebuffImmunity { get; set; }
        public int? ManaCost { get; set; }
        public int? Сooldown { get; set; }
        public string Description { get; set; }
        public Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}
