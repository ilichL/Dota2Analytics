using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2Analytics.Data.Entities.Enums
{
    public enum EventType
    {
        Kill,
        TowerDestroyed,
        BarracksDestroyed,
        RoshanKill,
        CourierKill,
        Buyback,
        FirstBlood,
        Rampage,
        WardKill,
        SmokeOfDeceitUsed,
        GemDrop,
        GemPickUp,
        RapierDrop,
        RapierPickUp,
        ItemBuy, //предмет
        IteamSell,
        ItemUsed,
        AegisSnatch, //украли аегис
        AbilityCast,
        RunePickUp,
        RuneSpawn,
        WisdomSpawn,
        WisdomPickUp,
        LotusSpawn,
        LotusPickUp,
        CreepSpawn,
        CreepWithcatapultSpawn,
        DayCycle,
        NightCycle
    }
}
