using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dota2Analytics.Infrastructure.Services.Implementations
{
    public class SteamWebAPIService
    {
        private readonly ILogger<SteamWebAPIService> logger;
        private readonly HttpClient httpClient;
        private readonly string steamApiKey;
        //https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V1/ вернет последние 100 матчей
        public SteamWebAPIService(IConfiguration config, ILogger<SteamWebAPIService> _logger)
        {
            logger = _logger;
            steamApiKey = config["SteamApiKey"];
        }

        public async Task GEtMatches()
        {//будет постоянно кидать этот запрос для актуализации матчей(нужен делей)
            string url = $"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V1/?key={steamApiKey}";
            var responce = await httpClient.GetAsync(url);
            var jsonText = await responce.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(jsonText);
            var result = json.RootElement.GetProperty("result");
            int status = result.GetProperty("status").GetInt32();

            if(CheckResultSuccess(status, url))
            {//проверка на BadRequest

            }
        }

        private bool CheckResultSuccess(int status, string url)
        {
            switch(status)
            {
                case 1:
                    {
                        return true;
                    }

                case 2:
                    {
                        logger.LogError("Invalid Request while parcing ,", url);
                        return false;
                    }

                case 8:
                case 15:
                    {
                        logger.LogError("No League Matches Found while parcing ,", url);
                        return false;
                    }

                case 101:
                case 100:
                    {
                        logger.LogError("Private Player Match History", url);
                        return false;
                    }


                case 400:
                    {
                        logger.LogError("Invalid Match ID while parcing ", url);
                        return false;
                    }

                case 403:
                    {
                        logger.LogError("Access Denied. SteamApiKey was banned(. ", DateTime.Now);
                        return false;
                    }

                case 503:
                case 500:
                    {
                        logger.LogError("Valve Server Error ", DateTime.Now);
                        return false;
                    }

                default:
                    {
                        logger.LogError("Unexpecting error while parcing ", url);
                        return false;
                    }
            }
        }
    }
}
