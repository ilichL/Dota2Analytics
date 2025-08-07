using Dota2Analytics.Data.Abstractions;
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
        public string Ability { get; set; }//точка, на героя и т д 
        public string Effects { get; set; }//действует на 
        public int Cost { get; set; }
        public bool IsActive { get; set; }
        public string? ActiveDescription { get; set; }
        public string? Type { get; set; }//пассивный, ненаправленный, направленный на существо
        public int? RecipeCost { get; set; }
        public int? ManaCost { get; set; }
        public int? Сooldown {  get; set; }
    }
}
