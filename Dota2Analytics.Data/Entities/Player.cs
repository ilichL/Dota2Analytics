using Dota2Analytics.Data.Abstractions;

namespace Dota2Analytics.Data.Entities
{
    public class Player : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsPrivateHistory { get; set; }
        public string NickName { get; set; }
        public long? SteamAccountId { get; set; }
        public int WinRate { get; set; }
        public int Rank { get; set; }
<<<<<<< HEAD
        public int? MatchEventId { get; set; }
=======
        public Guid? MatchEventId { get; set; }
>>>>>>> new-version
        public MatchEvent? MatchEvent { get; set; }
        public MatchPlayer? MatchPlayer { get; set; }
    }
}
