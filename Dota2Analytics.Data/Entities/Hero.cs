using Dota2Analytics.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class Hero : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Attribute { get; set; }//сила, ловкость, инетллект, универсал
        public string AttackType { get; set; }
        public string Tag { get; set; }//контроль, стойкость, побег, инициация, быстрый урон, осада
        public string Role { get; set; }//керри, сап, мидер, 3ка
        public string TalantTree { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }//сила
        public int StrengthIncrease { get; set; }
        public int Agility { get; set; }//ловкость
        public int AgilityIncrease { get; set; }
        public int Intelligence { get; set; }//интеллект
        public int IntelligenceIncrease { get; set; }
        public string InnateAbility { get; set; }//врожденка
        public int[] Damage { get; set; }//56-60
        public int Armor { get; set; }
        public int AttackInterval { get; set; }
        public int AttackSpeed { get; set; }
        public int AttackRange { get; set; }
        public int[] Aspect { get; set; }//максимумм 5
        public List<Skill> Skills { get; set; }
    }
}
