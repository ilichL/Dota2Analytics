# Базовый образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Сборка
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все .csproj файлы и восстанавливаем зависимости
COPY ["Dota2Analytics/Dota2Analytics.csproj", "Dota2Analytics/"]
COPY ["Dota2Analytics.Data/Dota2Analytics.Data.csproj", "Dota2Analytics.Data/"]
COPY ["Dota2Analytics.Infrastructure/Dota2Analytics.Infrastructure.csproj", "Dota2Analytics.Infrastructure/"]

RUN dotnet restore "Dota2Analytics/Dota2Analytics.csproj"

# Копируем весь код и собираем
COPY . .
WORKDIR "/src/Dota2Analytics"
RUN dotnet build "Dota2Analytics.csproj" -c Release -o /app/build

# Публикуем
FROM build AS publish
RUN dotnet publish "Dota2Analytics.csproj" -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dota2Analytics.dll"]