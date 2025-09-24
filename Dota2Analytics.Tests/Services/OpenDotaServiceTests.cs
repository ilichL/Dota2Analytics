using Dota2Analytics.Data.Entities;
using Dota2Analytics.Models.Enums;
using Dota2Analytics.Infrastructure.Repositories.Implementations;
using Dota2Analytics.Infrastructure.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;
using Match = Dota2Analytics.Data.Entities.Match;

namespace Dota2Analytics.Tests.Services;

public class OpenDotaAPIServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly Mock<HeroRepository> _heroRepositoryMock;
    private readonly Mock<MatchRepository> _matchRepositoryMock;
    private readonly Mock<ILogger<OpenDotaAPIService>> _loggerMock;
    private readonly OpenDotaAPIService _openDotaAPIService;

    public OpenDotaAPIServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _heroRepositoryMock = new Mock<HeroRepository>();
        _matchRepositoryMock = new Mock<MatchRepository>();
        _loggerMock = new Mock<ILogger<OpenDotaAPIService>>();

        _openDotaAPIService = new OpenDotaAPIService(
            _httpClient,
            _heroRepositoryMock.Object,
            _matchRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact] //это тест
    public async Task UpdatePlayerEmptySteamAccount()
    {
        //Arrange
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var heroRepository = new Mock<HeroRepository>();
        var matchRepository = new Mock<MatchRepository>();
        var logger = new Mock<ILogger<OpenDotaAPIService>>();
        var openDotaApiService = new OpenDotaAPIService(httpClient, heroRepository.Object, matchRepository.Object, _loggerMock.Object);
        string steamAccountId = "";

        //Act
        await openDotaApiService.UpdtaePlayerAsync("steamAccountId");

        //Assert
        _loggerMock.Verify(logger => logger.LogError(
            It.IsAny<string>()
            ), Times.Once);
    }
}