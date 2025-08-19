using Dota2Analytics.Data.Entities;
using Dota2Analytics.Data.Entities.Enums;
using Dota2Analytics.Infrastructure.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Services.Implementations
{
    public class OpenDotaAPIService
    {
        private readonly HttpClient httpClient;
        private readonly HeroRepository HeroRepository;

        public async Task UpdateHeroes()
        {
            string url = "https://api.opendota.com/api/heroStats";
            var response = await httpClient.GetAsync(url);
            var jsonText = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(jsonText);
        }

        public async Task ParseHeroes()
        {//в айпи все характеристики неправильные(как базовые, так и приросты и т д)
            string url = "https://api.opendota.com/api/heroStats";

            var response = await httpClient.GetAsync(url);

            var jsonText = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(jsonText);

            var heroes = new List<Hero>();
            json.RootElement.EnumerateArray().Select(hero => new Hero()
            {
                Id = Guid.NewGuid(),
                Attribute = hero.GetProperty("primary_attr").GetString() switch
                {
                    "agi" => HeroAttribute.Agility,
                    "all" => HeroAttribute.Universal,
                    "int" => HeroAttribute.Intelligence,
                    "str" => HeroAttribute.Strength
                },
                Name = hero.GetProperty("localized_name").GetString(),


            })

        public async Task GetMathes(string matchId)
        {//можно спарсить benchmarks
            string url = $"https://api.opendota.com/api/matches/{matchId}";

            var response = await httpClient.GetAsync(url);

            var jsonText = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(jsonText);


            var newMatchId = Guid.NewGuid();
            

            var match = new Match
            {
                Id = newMatchId,
                SteamMatchId = matchId,
                Duration = json.RootElement.GetProperty("duration").GetInt32(),//в секундах
                DireScore = json.RootElement.GetProperty("dire_score").GetInt32(),
                RadiantScore = json.RootElement.GetProperty("radiant_score").GetInt32(),
                Region = RegionSwitch(json.RootElement.GetProperty("region").GetInt32()),
                Mode = GameModeSwitch(json.RootElement.GetProperty("game_mode").GetInt32()),
                WinnerTeam = json.RootElement.GetProperty("radiant_win").GetBoolean() switch
                {
                    true => Team.Radiant,
                    false => Team.Dier
                },
                MatchPlayers = json.RootElement.GetProperty("players").EnumerateArray()
                .Select(player => new MatchPlayer()
                {
                    Id = Guid.NewGuid(),
                    Kills = player.GetProperty("kills").GetInt32(),
                    Death = player.GetProperty("deaths").GetInt32(),
                    Assists = player.GetProperty("assists").GetInt32(),
                    CreepsLastHit = player.GetProperty("last_hits").GetInt32(),
                    CreepsDenies = player.GetProperty("denies").GetInt32(),
                    Gpm = player.GetProperty("gold_per_min").GetDecimal(),
                    Xpm = player.GetProperty("xp_per_min").GetDecimal(),
                    PlayerLevel = player.GetProperty("level").GetInt32(),
                    NetWorth = player.GetProperty("net_worth").GetInt32(),
                    HeroDamage = player.GetProperty("hero_damage").GetInt32(),
                    TowerDamage = player.GetProperty("tower_damage").GetInt32(),
                    HeroHealing = player.GetProperty("hero_healing").GetInt32(),
                    KillPerMinute = player.GetProperty("kills_per_min").GetDecimal(),
                    Kda = player.GetProperty("kda").GetDecimal(),
                    Win = player.GetProperty("win").GetInt32(),
                    HeroHealingPerMinute = player.GetProperty("benchmarks").GetProperty("hero_healing_per_min")
                        .GetProperty("raw").GetDecimal(),
                    HeroDamagePerMinute = player.GetProperty("benchmarks").GetProperty("hero_damage_per_min")
                        .GetProperty("raw").GetDecimal(),
                    Team = player.GetProperty("isRadiant").GetBoolean() switch
                    {
                        true => Team.Radiant,
                        false => Team.Dier
                    },
                    MatchId = newMatchId,
                    //TowerDamagePerMinute = TowerDamage / Duration, / 60
                    // Hero = await HeroSwitch(player.GetProperty("hero_id").GetInt32()),


                }).ToList()
            };
        }

        private string RegionSwitch(int regionNumber)
        {
            switch (regionNumber) {
                case 8: return "Russia";
                default: return null;
            }
        }

        private string GameModeSwitch(int gameModeNumber)
        {
            switch (gameModeNumber)
            {
                case 4: return "Single Draft";
                default: return null;
            }
        }

       
    }
}
