using Dota2Analytics.Data.Entities;
using Dota2Analytics.Data.Entities.Enums;
using Dota2Analytics.Infrastructure.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = Dota2Analytics.Data.Entities.Match;

namespace Dota2Analytics.Infrastructure.Services.Implementations
{
    public class OpenDotaAPIService
    {
        private readonly HttpClient httpClient;
        private readonly HeroRepository HeroRepository;
        private readonly MatchRepository MatchRepository;

        public async Task UpdtaePlayer(string steamAccountId)
        {
            string url = $"https://api.opendota.com/api/players/{steamAccountId}";
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
                OpenDotaId = hero.GetProperty("id").GetInt32(),
                Attribute = hero.GetProperty("primary_attr").GetString() switch
                {
                    "agi" => HeroAttribute.Agility,
                    "all" => HeroAttribute.Universal,
                    "int" => HeroAttribute.Intelligence,
                    "str" => HeroAttribute.Strength
                },
                Name = hero.GetProperty("localized_name").GetString(),
                AttackType = hero.GetProperty("attack_type").GetString() switch
                {
                    "Ranged" => AttackType.Ranged,
                    "Melee" => AttackType.Meel
                },
                Roles = GetRoles(hero.GetProperty("roles").EnumerateArray().Select(role => role.ToString()).ToList()),
                HeroTags = GetTags(hero.GetProperty("roles").EnumerateArray().Select(role => role.ToString()).ToList()),
                DayVision = hero.GetProperty("day_vision").GetInt32(),
                NightVision = hero.GetProperty("night_vision").GetInt32()

            }).ToList();

            await HeroRepository.AddRange(heroes);
        }

        public async Task GetMathes(string matchId)
        {//можно спарсить benchmarks
            string url = $"https://api.opendota.com/api/matches/{matchId}";
            try
            {
                var response = await httpClient.GetAsync(url);

                if(!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Косяяк");//логер
                    return;
                }

                var jsonText = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonText);
                var newMatchId = Guid.NewGuid();

                var match = new Data.Entities.Match
                {
                    Id = newMatchId,
                    SteamMatchId = matchId,
                    Duration = json.RootElement.GetProperty("duration   ").GetInt32(),//в секундах
                    DireScore = json.RootElement.GetProperty("dire_score").GetInt32(),
                    RadiantScore = json.RootElement.GetProperty("radiant_score").GetInt32(),
                    Region = RegionSwitch(json.RootElement.GetProperty("region").GetInt32()),
                    Mode = GameModeSwitch(json.RootElement.GetProperty("game_mode").GetInt32()),

                    WinnerTeam = json.RootElement.GetProperty("radiant_win").GetBoolean() switch
                    {
                        true => Team.Radiant,
                        false => Team.Dire
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
                            false => Team.Dire
                        },
                        MatchId = newMatchId,

                    }).ToList()

                };
                var matchHeroesIds = json.RootElement.GetProperty("players").EnumerateArray()
                    .Select(player => (int?)player.GetProperty("hero_id").GetInt32()).ToList();
                var heroesList = await HeroRepository.GetHeroesByOpenDotaIds(matchHeroesIds);

                for (int i = 0; i < match.MatchPlayers.Count; i++)
                {
                    match.MatchPlayers[i].Hero = heroesList[i];
                    match.MatchPlayers[i].HeroId = heroesList[i].Id;
                }


                await MatchRepository.AddAsync(match);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);// сделать нормальный логгер
            }

            
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

        private List<HeroRole?> GetRoles(List<string> roles)
        {
            var rolesList = new List<HeroRole?>();
            foreach (var role in roles)
            {
                switch (role)
                {
                    case "Carry":
                    {
                            rolesList.Add(HeroRole.Carry);
                            break;
                    }

                    case "Support":
                    {
                            rolesList.Add(HeroRole.Support);
                            break;
                    }

                    default: break;
                }

            }

            return rolesList;
        }

        private List<HeroTag?> GetTags(List<string> tags)
        {
            var tagsList = new List<HeroTag?>();
            foreach (var tag in tags)
            {
                switch (tag)
                {
                    case "Durable":
                    {
                            tagsList.Add(HeroTag.Durable);
                            break;
                    }

                    case "Initiator":
                    {
                            tagsList.Add(HeroTag.Initiation);
                            break;
                    }

                    case "Disabler":
                    {
                            tagsList.Add(HeroTag.Disable);
                            break;
                    }

                    case "Escape":
                    {
                            tagsList.Add(HeroTag.Escape);
                            break;
                    }

                    case "Nuker":
                    {
                            tagsList.Add(HeroTag.Nuker);
                            break;
                    }

                    case "Pusher":
                    {
                            tagsList.Add(HeroTag.Pusher);
                            break;
                    }
                }
            }

            return tagsList;
        }
       
    }
}
