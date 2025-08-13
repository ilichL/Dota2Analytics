using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dota2Analytics.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Iteams_MatchPlayers_MatchPlayerId",
                table: "Iteams");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Matches_MatchId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_MatchId",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Iteams",
                table: "Iteams");

            migrationBuilder.DropIndex(
                name: "IX_Iteams_MatchPlayerId",
                table: "Iteams");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MathId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Heroes");

            migrationBuilder.RenameTable(
                name: "Iteams",
                newName: "Items");

            migrationBuilder.RenameColumn(
                name: "Сooldown",
                table: "Skills",
                newName: "Cooldown");

            migrationBuilder.RenameColumn(
                name: "Deth",
                table: "MatchPlayers",
                newName: "Death");

            migrationBuilder.RenameColumn(
                name: "DamageRecivedReduced",
                table: "MatchPlayers",
                newName: "DamageReceivedReduced");

            migrationBuilder.RenameColumn(
                name: "DamageRecivedRaw",
                table: "MatchPlayers",
                newName: "DamageReceivedRaw");

            migrationBuilder.RenameColumn(
                name: "DierScore",
                table: "Matches",
                newName: "DireScore");

            migrationBuilder.RenameColumn(
                name: "BestWintstrick",
                table: "HeroStats",
                newName: "BestWinStreak");

            migrationBuilder.RenameColumn(
                name: "TalantTree",
                table: "Heroes",
                newName: "TalentTree");

            // В Items «рокировка» колонок: старая кириллическая "Сooldown" -> IteamPurchaseId, а MatchPlayerId -> Cooldown
            migrationBuilder.RenameColumn(
                name: "Сooldown",
                table: "Items",
                newName: "IteamPurchaseId");

            migrationBuilder.RenameColumn(
                name: "MatchPlayerId",
                table: "Items",
                newName: "Cooldown");

            // 1) MatchEvents.EventType: text -> integer с явным маппингом строк на числовые значения enum
            migrationBuilder.Sql("""
                ALTER TABLE "MatchEvents"
                ALTER COLUMN "EventType" TYPE integer
                USING (
                    CASE "EventType"
                        WHEN 'Kill'   THEN 0
                        WHEN 'Death'  THEN 1
                        WHEN 'Assist' THEN 2
                        ELSE 0
                    END
                );
            """);

            // Доп. колонка RuneType (enum)
            migrationBuilder.AddColumn<int>(
                name: "RuneType",
                table: "MatchEvents",
                type: "integer",
                nullable: true);

            // 2) Matches.WinnerTeam: text -> integer (БД пустая, поэтому безопасный ::integer)
            migrationBuilder.Sql("""
                ALTER TABLE "Matches"
                ALTER COLUMN "WinnerTeam" TYPE integer
                USING "WinnerTeam"::integer;
            """);

            // 3) Heroes.Role: text -> integer (nullable)
            migrationBuilder.Sql("""
                ALTER TABLE "Heroes"
                ALTER COLUMN "Role" TYPE integer
                USING ("Role")::integer;
            """);

            // 4) Heroes.Attribute: text -> integer
            migrationBuilder.Sql("""
                ALTER TABLE "Heroes"
                ALTER COLUMN "Attribute" TYPE integer
                USING "Attribute"::integer;
            """);

            // 5) Heroes.AttackType: text -> integer с явным маппингом (Meel/Ranged)
            migrationBuilder.Sql("""
                ALTER TABLE "Heroes"
                ALTER COLUMN "AttackType" TYPE integer
                USING (
                    CASE "AttackType"
                        WHEN 'Meel'  THEN 0   -- как в твоём enum AttackType
                        WHEN 'Melee' THEN 0   -- на случай, если раньше писали корректно
                        WHEN 'Ranged' THEN 1
                        ELSE 0
                    END
                );
            """);

            // Добавляем HeroTag (enum, новая колонка)
            migrationBuilder.AddColumn<int>(
                name: "HeroTag",
                table: "Heroes",
                type: "integer",
                nullable: true);

            // 6) Items.Ability: text -> integer
            migrationBuilder.Sql("""
                ALTER TABLE "Items"
                ALTER COLUMN "Ability" TYPE integer
                USING "Ability"::integer;
            """);

            // Новое булево поле
            migrationBuilder.AddColumn<bool>(
                name: "IsPossibleToEat",
                table: "Items",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            // Связующая таблица составных предметов
            migrationBuilder.CreateTable(
                name: "IteamIteam",
                columns: table => new
                {
                    ParentIteamsId = table.Column<int>(type: "integer", nullable: false),
                    UsedInItemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IteamIteam", x => new { x.ParentIteamsId, x.UsedInItemsId });
                    table.ForeignKey(
                        name: "FK_IteamIteam_Items_ParentIteamsId",
                        column: x => x.ParentIteamsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IteamIteam_Items_UsedInItemsId",
                        column: x => x.UsedInItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Таблица покупок предметов
            migrationBuilder.CreateTable(
                name: "ItemPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PurchaseTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSold = table.Column<bool>(type: "boolean", nullable: false),
                    SoldTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WasEaten = table.Column<bool>(type: "boolean", nullable: true),
                    EatTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    IteamId = table.Column<int>(type: "integer", nullable: false),
                    MatchPlayerId = table.Column<int>(type: "integer", nullable: false),
                    MatchEventId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPurchases_Items_IteamId",
                        column: x => x.IteamId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPurchases_MatchEvents_MatchEventId",
                        column: x => x.MatchEventId,
                        principalTable: "MatchEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemPurchases_MatchPlayers_MatchPlayerId",
                        column: x => x.MatchPlayerId,
                        principalTable: "MatchPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IteamIteam_UsedInItemsId",
                table: "IteamIteam",
                column: "UsedInItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchases_IteamId",
                table: "ItemPurchases",
                column: "IteamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchases_MatchEventId",
                table: "ItemPurchases",
                column: "MatchEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchases_MatchPlayerId",
                table: "ItemPurchases",
                column: "MatchPlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IteamIteam");

            migrationBuilder.DropTable(
                name: "ItemPurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RuneType",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "HeroTag",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "IsPossibleToEat",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Iteams");

            migrationBuilder.RenameColumn(
                name: "Cooldown",
                table: "Skills",
                newName: "Сooldown");

            migrationBuilder.RenameColumn(
                name: "Death",
                table: "MatchPlayers",
                newName: "Deth");

            migrationBuilder.RenameColumn(
                name: "DamageReceivedReduced",
                table: "MatchPlayers",
                newName: "DamageRecivedReduced");

            migrationBuilder.RenameColumn(
                name: "DamageReceivedRaw",
                table: "MatchPlayers",
                newName: "DamageRecivedRaw");

            migrationBuilder.RenameColumn(
                name: "DireScore",
                table: "Matches",
                newName: "DierScore");

            migrationBuilder.RenameColumn(
                name: "BestWinStreak",
                table: "HeroStats",
                newName: "BestWintstrick");

            migrationBuilder.RenameColumn(
                name: "TalentTree",
                table: "Heroes",
                newName: "TalantTree");

            migrationBuilder.RenameColumn(
                name: "IteamPurchaseId",
                table: "Iteams",
                newName: "Сooldown");

            migrationBuilder.RenameColumn(
                name: "Cooldown",
                table: "Iteams",
                newName: "MatchPlayerId");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MathId",
                table: "Players",
                type: "integer",
                nullable: true);

            // Откаты типов обратно в text (PG умеет int->text без USING)
            migrationBuilder.AlterColumn<string>(
                name: "EventType",
                table: "MatchEvents",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "WinnerTeam",
                table: "Matches",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Heroes",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Attribute",
                table: "Heroes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "AttackType",
                table: "Heroes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Heroes",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ability",
                table: "Iteams",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Iteams",
                table: "Iteams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_MatchId",
                table: "Players",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Iteams_MatchPlayerId",
                table: "Iteams",
                column: "MatchPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Iteams_MatchPlayers_MatchPlayerId",
                table: "Iteams",
                column: "MatchPlayerId",
                principalTable: "MatchPlayers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Matches_MatchId",
                table: "Players",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id");
        }
    }
}
