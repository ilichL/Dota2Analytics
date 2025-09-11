using Dota2Analytics.Data.Abstractions;

namespace Dota2Analytics.Data.Entities
{
    public class HeroStats : BaseEntity
    {
        public int WinRate { get; set; }
        public int BestWinStreak { get; set; }
        public int AverageGpm { get; set; }//gold per minute
        public int AverageXpm { get; set; }// Experienceper minute
        public int HeroId { get; set; }
        public Hero Hero { get; set; }

    }
}
