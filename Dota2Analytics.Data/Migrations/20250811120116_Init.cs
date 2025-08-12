using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dota2Analytics.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SteamMatchId = table.Column<string>(type: "text", nullable: false),
                    DierScore = table.Column<int>(type: "integer", nullable: false),
                    RadiantScore = table.Column<int>(type: "integer", nullable: false),
                    WinnerTeam = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Mode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Attribute = table.Column<string>(type: "text", nullable: false),
                    AttackType = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    TalantTree = table.Column<string>(type: "text", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Mana = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    StrengthIncrease = table.Column<int>(type: "integer", nullable: false),
                    Agility = table.Column<int>(type: "integer", nullable: false),
                    AgilityIncrease = table.Column<int>(type: "integer", nullable: false),
                    Intelligence = table.Column<int>(type: "integer", nullable: false),
                    IntelligenceIncrease = table.Column<int>(type: "integer", nullable: false),
                    InnateAbility = table.Column<string>(type: "text", nullable: true),
                    Damage = table.Column<int[]>(type: "integer[]", nullable: false),
                    Armor = table.Column<int>(type: "integer", nullable: false),
                    AttackInterval = table.Column<int>(type: "integer", nullable: false),
                    AttackSpeed = table.Column<int>(type: "integer", nullable: false),
                    AttackRange = table.Column<int>(type: "integer", nullable: false),
                    Aspect = table.Column<int[]>(type: "integer[]", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchEvents_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WinRate = table.Column<int>(type: "integer", nullable: false),
                    BestWintstrick = table.Column<int>(type: "integer", nullable: false),
                    AverageGpm = table.Column<int>(type: "integer", nullable: false),
                    AverageXpm = table.Column<int>(type: "integer", nullable: false),
                    HeroId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroStats_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DamageType = table.Column<string>(type: "text", nullable: true),
                    Range = table.Column<int>(type: "integer", nullable: true),
                    Affects = table.Column<string>(type: "text", nullable: true),
                    Dispellable = table.Column<bool>(type: "boolean", nullable: true),
                    PiercesDebuffImmunity = table.Column<bool>(type: "boolean", nullable: true),
                    ManaCost = table.Column<int>(type: "integer", nullable: true),
                    Сooldown = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    HeroId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NickName = table.Column<string>(type: "text", nullable: false),
                    SteamAccountId = table.Column<int>(type: "integer", nullable: true),
                    WinRate = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    MatchEventId = table.Column<int>(type: "integer", nullable: true),
                    MathId = table.Column<int>(type: "integer", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_MatchEvents_MatchEventId",
                        column: x => x.MatchEventId,
                        principalTable: "MatchEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Players_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kills = table.Column<int>(type: "integer", nullable: true),
                    Deth = table.Column<int>(type: "integer", nullable: true),
                    Assists = table.Column<int>(type: "integer", nullable: true),
                    BuildingDamage = table.Column<int>(type: "integer", nullable: true),
                    HeroDamage = table.Column<int>(type: "integer", nullable: true),
                    DamageRecivedRaw = table.Column<int>(type: "integer", nullable: true),
                    DamageRecivedReduced = table.Column<int>(type: "integer", nullable: true),
                    Pick = table.Column<int>(type: "integer", nullable: false),
                    SupportGoldSpent = table.Column<int>(type: "integer", nullable: true),
                    CampsStacked = table.Column<int>(type: "integer", nullable: true),
                    NetWorth = table.Column<int>(type: "integer", nullable: false),
                    CreepsLastHit = table.Column<int>(type: "integer", nullable: true),
                    CreepsDenies = table.Column<int>(type: "integer", nullable: true),
                    BountyRunes = table.Column<int>(type: "integer", nullable: true),
                    Gpm = table.Column<int>(type: "integer", nullable: false),
                    Xpm = table.Column<int>(type: "integer", nullable: false),
                    OutpostCaptured = table.Column<int>(type: "integer", nullable: true),
                    Heal = table.Column<int>(type: "integer", nullable: true),
                    PlayerLevel = table.Column<int>(type: "integer", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: true),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    SupportContribution = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Iteams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Ability = table.Column<string>(type: "text", nullable: false),
                    Effects = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveDescription = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    RecipeCost = table.Column<int>(type: "integer", nullable: true),
                    ManaCost = table.Column<int>(type: "integer", nullable: true),
                    Сooldown = table.Column<int>(type: "integer", nullable: true),
                    MatchPlayerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iteams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Iteams_MatchPlayers_MatchPlayerId",
                        column: x => x.MatchPlayerId,
                        principalTable: "MatchPlayers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_MatchId",
                table: "Heroes",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroStats_HeroId",
                table: "HeroStats",
                column: "HeroId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Iteams_MatchPlayerId",
                table: "Iteams",
                column: "MatchPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvents_MatchId",
                table: "MatchEvents",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_HeroId",
                table: "MatchPlayers",
                column: "HeroId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_MatchId",
                table: "MatchPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_PlayerId",
                table: "MatchPlayers",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_MatchEventId",
                table: "Players",
                column: "MatchEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_MatchId",
                table: "Players",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_HeroId",
                table: "Skills",
                column: "HeroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroStats");

            migrationBuilder.DropTable(
                name: "Iteams");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "MatchEvents");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
