using Dota2Analytics.Data.Abstractions;
<<<<<<< HEAD
using Dota2Analytics.Data.Entities.Enums;
=======
using Dota2Analytics.Models.Enums;
>>>>>>> new-version

namespace Dota2Analytics.Data.Entities
{
    public class Hero : BaseEntity
    {//openDota неправильные данные, так что приходится пока что терпеить много ?
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public HeroAttribute Attribute { get; set; }//сила, ловкость, инетллект, универсал
        public AttackType AttackType { get; set; }//ближний/дальний
        public List<HeroTag?> HeroTags { get; set; }
        public List<HeroRole?> Roles { get; set; }//керри, сап, мидер, 3ка
        public string? TalentTree { get; set; }
        public int? Health { get; set; } //базовые статы героя
        public decimal? HealthRegen { get; set; }
        public int? Mana { get; set; }
        public decimal? ManaRegen { get; set; }
        public int? Strength { get; set; }//сила
        public int? StrengthIncrease { get; set; }
        public int? Agility { get; set; }//ловкость
        public int? AgilityIncrease { get; set; }
        public int? Intelligence { get; set; }//интеллект
        public int? IntelligenceIncrease { get; set; }
        public string? InnateAbility { get; set; }//врожденка
        public int? MinDamage { get; set; }
        public int? MaxDamage { get; set; }
        public int? Armor { get; set; }
        public int? AttackInterval { get; set; }
        public int? AttackSpeed { get; set; }
        public int? AttackRange { get; set; }
        public int? MooveSpeed { get; set; }
        public int[]? Aspect { get; set; }//максимумм 5
        public int? OpenDotaId { get; set; }
        public int DayVision { get; set; }
        public int NightVision { get; set; }
        public List<Skill>? Skills { get; set; }
<<<<<<< HEAD
        public HeroStats? HeroStats { get; set; }
=======
        public virtual HeroStats? HeroStats { get; set; }
>>>>>>> new-version
        public MatchPlayer? MatchPlayer { get; set; }
    }
}
