using Dota2Analytics.Data.Entities;
using Dota2Analytics.Data.Entities.Enums;
using Dota2Analytics.Infrastructure.Repositories.Abstractions;
using Dota2Analytics.Infrastructure.Repositories.Implementations;
using Dota2Analytics.Models.OpenDota;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json;

namespace Dota2Analytics.Infrastructure.Services.Implementations
{
    public class OpenDotaAPIService
    {
        private readonly HttpClient httpClient;
        private readonly IHeroRepository _heroRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<OpenDotaAPIService> logger;


        public OpenDotaAPIService(HttpClient httpClient, IHeroRepository heroRepository, IMatchRepository matchRepository,
             IPlayerRepository playerRepository, ILogger<OpenDotaAPIService> logger)
        {
            this.httpClient = httpClient;
            _heroRepository = heroRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            this.logger = logger;
        }
        public async Task UpdtaePlayer(string steamAccountId)
        {
            string url = $"https://api.opendota.com/api/players/{steamAccountId}";
            var response = await httpClient.GetAsync(url);
            var jsonText = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(jsonText);

        }

        public async Task<OpenDotaPlayerDto>? UpdtaePlayerAsync(string steamAccountId)
        {
            try
            {
                var openDotaId = GetOpenDotaId(steamAccountId);
                string url = $"https://api.opendota.com/api/players/{openDotaId}";
                var response = await httpClient.GetAsync(url);
                var jsonText = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonText);
                var player = await _playerRepository.GetPlayerBySteamIdAsync(long.Parse(steamAccountId));//System.NullReferenceException: "Object reference not set to an instance of an object."

                var playerName = json.RootElement.GetProperty("profile").GetProperty("personaname").GetString();
                var playerNickName = json.RootElement.GetProperty("profile").GetProperty("name").GetString();

                if (playerName is not null)
                {
                    player.Name = playerNickName;
                }

                if (playerNickName is not null)
                {
                    player.NickName = playerNickName;
                }

                await _playerRepository.UpdateAsync(player);

                var result = new OpenDotaPlayerDto()
                {
                    Name = playerName,
                    NickName = playerNickName,
                    OpenDotaId = openDotaId
                };

                return result;
            }

            catch (Exception ex)
            {
                logger.LogError($"OpenDotaAPIService error in UpdtaePlayer, with steamAccountId = {steamAccountId} and exception: {ex}");
                return null;
            }

        }

        public async Task<List<OpenDotaHeroDto>>? ParseHeroesAsync()
        {//в айпи все характеристики неправильные(как базовые, так и приросты и т д)
            try
            {
                string url = "https://api.opendota.com/api/heroStats";

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"Error while parsing in OpendotaApiService, ParseHeroesAsync with status code: {response.StatusCode}");
                    return null;
                }

                var jsonText = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonText);

                var heroes = json.RootElement.EnumerateArray().Select(hero => new Hero()
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
                        "Melee" => AttackType.Melee
                    },
                    Roles = GetRoles(hero.GetProperty("roles").EnumerateArray().Select(role => role.ToString()).ToList()),
                    HeroTags = GetTags(hero.GetProperty("roles").EnumerateArray().Select(role => role.ToString()).ToList()),
                    DayVision = hero.GetProperty("day_vision").GetInt32(),
                    NightVision = hero.GetProperty("night_vision").GetInt32()

                }).ToList();

                await _heroRepository.AddRange(heroes);

                var result = heroes.Select(hero => new OpenDotaHeroDto()
                {
                    OpenDotaId = hero.OpenDotaId,
                    Name = hero.Name,
                    Attribute = hero.Attribute switch
                    {
                        HeroAttribute.Agility => "Agility",
                        HeroAttribute.Strength => "Strength",
                        HeroAttribute.Intelligence => "Intelligence",
                        HeroAttribute.Universal => "Universal"
                    },
                    //Roles = hero.Roles
                    AttackType = hero.AttackType switch
                    {
                        AttackType.Ranged => "Ranged",
                        AttackType.Melee => "Melee"
                    },
                    DayVision = hero.DayVision,
                    NightVision= hero.NightVision
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError($"OpendotaApiService, ParseHeroesAsync with error: {ex}");
                return null;
            }

        }

        public async Task<List<OpenDotaMatch>>? GetMatchesByUserSteamIdAsync(string steamAccountId)
        {
            long openDotaId = GetOpenDotaId(steamAccountId);
            string url = $"https://api.opendota.com/api/players/{steamAccountId}/matches";
            try
            {
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"OpendotaApiService, parse match(GetMathesByUserSteamId) by url: {url}" +
                        $"with status code: {response.StatusCode}");
                    return null;
                }

                var jsonText = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonText);

                var playerMatches = new List<Match>();

                foreach (var jsonRoot in json.RootElement.EnumerateArray())
                {
                    playerMatches.Add(new Match
                    {
                        Id = Guid.NewGuid(),
                        SteamMatchId = jsonRoot.GetProperty("match_id").ToString(),
                        WinnerTeam = jsonRoot.GetProperty("radiant_win").GetBoolean() switch
                        {
                            false => Team.Radiant,
                            true => Team.Dire
                        },
                        Mode = GameModeSwitch(jsonRoot.GetProperty("mode").GetInt32()),
                        Duration = jsonRoot.GetProperty("duration").GetInt32()//секунды

                    });
                }

                await _matchRepository.UpdateRange(playerMatches);

                var result = playerMatches.Select(match => new OpenDotaMatch()
                {
                    SteamMatchId = match.SteamMatchId,
                    WinnerTeam = match.WinnerTeam switch
                    {
                        Team.Radiant => "Radiant",
                        Team.Dire => "Dire"
                    },
                    Mode = match.Mode,
                    Duration = match.Duration,
                }).ToList();

                return result;
            }

            catch (Exception ex)
            {
                logger.LogError($"OpendotaApiService, parse match(GetMathesByUserSteamId) by url: {url}, with error:{ex}");
                return null;
            }
        }

        public async Task GetMatсhAsync(string matchId)
        {//можно спарсить benchmarks
            string url = $"https://api.opendota.com/api/matches/{matchId}";
            try
            {
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError("Error while parsing OpendotaApiService, " +
                        "GetMatheAsync, url: ", url, " with status code: ", response.StatusCode);
                    return;
                }

                var jsonText = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonText);
                var newMatchId = Guid.NewGuid();

                var match = new Data.Entities.Match
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
                var heroesList = await _heroRepository.GetHeroesByOpenDotaIds(matchHeroesIds);

                for (int i = 0; i < match.MatchPlayers.Count; i++)
                {
                    match.MatchPlayers[i].Hero = heroesList[i];
                    match.MatchPlayers[i].HeroId = heroesList[i].Id;
                }

                await _matchRepository.AddAsync(match);
            }
            catch (Exception ex)
            {
                logger.LogError("OpendotaApiService, parse match(GetMatсhAsync), with error: ", ex);
            }


        }

        private string RegionSwitch(int regionNumber)
        {
            switch (regionNumber) {
                case 8: return "Russia";
                default:
                    {
                        logger.LogError("OpenDotaApiService, RegionSwitch, with regionNumber: ", regionNumber);
                        return null;
                    } 
            }
        }

        private string GameModeSwitch(int gameModeNumber)
        {
            switch (gameModeNumber)
            {
                case 4: return "Single Draft";
                case 22: return "Ranked";
                default:
                    {
                        logger.LogError("OpenDotaApiService, GameModeSwitch, with gameModeNumber: ", gameModeNumber);
                        return null;
                    }
                        
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

                    default: break;
                }
            }

            return tagsList;
        }

        private long GetOpenDotaId(string steamAccountId)
        {
            return 76561198815525464 - long.Parse(steamAccountId);
        }

    }
}
