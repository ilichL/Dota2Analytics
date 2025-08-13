using Dota2Analytics.Data.Abstractions;
using Dota2Analytics.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities
{
    public class Iteam : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IteamAbility Ability { get; set; }//точка, на героя и т д 
        public string Effects { get; set; }//действует на 
        public int Cost { get; set; }
        public bool IsActive { get; set; }
        public bool IsPossibleToEat { get; set; }//аганим, шард, муншард
        public string? ActiveDescription { get; set; }
        public string? Type { get; set; }//пассивный, ненаправленный, направленный на существо
        public int? RecipeCost { get; set; }
        public int? ManaCost { get; set; }
        public int? Cooldown {  get; set; }
        public List<Iteam>? ParentIteams { get; set; }//предметы из которых был создан
        public List<Iteam>? UsedInItems { get; set; }//предметы в которые потанцевально может собраться 
        public int? IteamPurchaseId { get; set; }
        public ItemPurchase? ItemPurchase { get; set; }//предмет новый, статистики нет 
        public int NumberOfPurchases { get; set; }//сколько раз купили для статистики 
    }
}
