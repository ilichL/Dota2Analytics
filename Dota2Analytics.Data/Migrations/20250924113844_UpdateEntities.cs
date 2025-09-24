using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dota2Analytics.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Matches_MatchId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayers_Players_PlayerId",
                table: "MatchPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_MatchId",
                table: "Heroes");

            migrationBuilder.RenameColumn(
                name: "Heal",
                table: "MatchPlayers",
                newName: "TowerDamage");

            migrationBuilder.RenameColumn(
                name: "BuildingDamage",
                table: "MatchPlayers",
                newName: "HeroHealing");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Heroes",
                newName: "OpenDotaId");

            migrationBuilder.RenameColumn(
                name: "MatchId",
                table: "Heroes",
                newName: "MooveSpeed");

            migrationBuilder.RenameColumn(
                name: "HeroTag",
                table: "Heroes",
                newName: "MinDamage");

            migrationBuilder.RenameColumn(
                name: "Damage",
                table: "Heroes",
                newName: "Roles");

            // Безопасные изменения типов (не ID)
            migrationBuilder.AlterColumn<long>(
                name: "SteamAccountId",
                table: "Players",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivateHistory",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Безопасные изменения числовых типов
            migrationBuilder.AlterColumn<decimal>(
                name: "Xpm",
                table: "MatchPlayers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gpm",
                table: "MatchPlayers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            // Разрешаем NULL для внешних ключей временно
            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "MatchPlayers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "MatchPlayers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DotaBuffId",
                table: "MatchPlayers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeroDamagePerMinute",
                table: "MatchPlayers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeroHealingPerMinute",
                table: "MatchPlayers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Kda",
                table: "MatchPlayers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "KillPerMinute",
                table: "MatchPlayers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Team",
                table: "MatchPlayers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TowerDamagePerMinute",
                table: "MatchPlayers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Win",
                table: "MatchPlayers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDate",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Matches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPurchases",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Безопасное изменение времени (DateTime → int секунды)
            migrationBuilder.Sql(@"
                ALTER TABLE ""ItemPurchases"" ADD COLUMN ""NewPurchaseTime"" integer;
                UPDATE ""ItemPurchases"" SET ""NewPurchaseTime"" = EXTRACT(epoch FROM ""PurchaseTime"");
                ALTER TABLE ""ItemPurchases"" DROP COLUMN ""PurchaseTime"";
                ALTER TABLE ""ItemPurchases"" RENAME COLUMN ""NewPurchaseTime"" TO ""PurchaseTime"";
                ALTER TABLE ""ItemPurchases"" ALTER COLUMN ""PurchaseTime"" SET NOT NULL;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""ItemPurchases"" ADD COLUMN ""NewSoldTime"" integer;
                UPDATE ""ItemPurchases"" SET ""NewSoldTime"" = EXTRACT(epoch FROM ""SoldTime"");
                ALTER TABLE ""ItemPurchases"" DROP COLUMN ""SoldTime"";
                ALTER TABLE ""ItemPurchases"" RENAME COLUMN ""NewSoldTime"" TO ""SoldTime"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""ItemPurchases"" ADD COLUMN ""NewEatTime"" integer;
                UPDATE ""ItemPurchases"" SET ""NewEatTime"" = EXTRACT(epoch FROM ""EatTime"");
                ALTER TABLE ""ItemPurchases"" DROP COLUMN ""EatTime"";
                ALTER TABLE ""ItemPurchases"" RENAME COLUMN ""NewEatTime"" TO ""EatTime"";
            ");

            // Безопасные изменения для Heroes (делаем поля nullable)
            migrationBuilder.AlterColumn<string>(
                name: "TalentTree",
                table: "Heroes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "StrengthIncrease",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Strength",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Mana",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "IntelligenceIncrease",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Intelligence",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Health",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AttackSpeed",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AttackRange",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AttackInterval",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Armor",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AgilityIncrease",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Agility",
                table: "Heroes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DayVision",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "HealthRegen",
                table: "Heroes",
                type: "numeric",
                nullable: true);

            // ИСПРАВЛЕННЫЙ БЛОК: Конвертация HeroTags в массив для PostgreSQL
            migrationBuilder.AddColumn<int[]>(
                name: "HeroTags",
                table: "Heroes",
                type: "integer[]",
                nullable: true);

            // Обновляем данные после добавления столбца
            migrationBuilder.Sql(@"
                UPDATE ""Heroes"" SET ""HeroTags"" = CASE 
                    WHEN ""Roles"" IS NOT NULL THEN ARRAY[""Roles""] 
                    ELSE '{}' 
                END;
            ");

            migrationBuilder.AddColumn<decimal>(
                name: "ManaRegen",
                table: "Heroes",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxDamage",
                table: "Heroes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NightVision",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Method = table.Column<int>(type: "integer", nullable: false),
                    QueryString = table.Column<string>(type: "text", nullable: true),
                    ClientIP = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    StatusCode = table.Column<int>(type: "integer", nullable: false),
                    ResponseTimeMs = table.Column<long>(type: "bigint", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });

            // Восстанавливаем внешние ключи
            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayers_Players_PlayerId",
                table: "MatchPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            // Восстанавливаем ограничение NOT NULL после миграции
            migrationBuilder.Sql(@"
                UPDATE ""MatchPlayers"" SET ""PlayerId"" = 0 WHERE ""PlayerId"" IS NULL;
                ALTER TABLE ""MatchPlayers"" ALTER COLUMN ""PlayerId"" SET NOT NULL;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayers_Players_PlayerId",
                table: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "RequestLogs");

            // Обратные изменения...
            migrationBuilder.DropColumn(
                name: "IsPrivateHistory",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "DotaBuffId",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "HeroDamagePerMinute",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "HeroHealingPerMinute",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Kda",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "KillPerMinute",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "TowerDamagePerMinute",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "Win",
                table: "MatchPlayers");

            migrationBuilder.DropColumn(
                name: "MatchDate",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "NumberOfPurchases",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DayVision",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "HealthRegen",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "HeroTags",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "ManaRegen",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "MaxDamage",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "NightVision",
                table: "Heroes");

            migrationBuilder.RenameColumn(
                name: "TowerDamage",
                table: "MatchPlayers",
                newName: "Heal");

            migrationBuilder.RenameColumn(
                name: "HeroHealing",
                table: "MatchPlayers",
                newName: "BuildingDamage");

            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Heroes",
                newName: "Damage");

            migrationBuilder.RenameColumn(
                name: "OpenDotaId",
                table: "Heroes",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "MooveSpeed",
                table: "Heroes",
                newName: "MatchId");

            migrationBuilder.RenameColumn(
                name: "MinDamage",
                table: "Heroes",
                newName: "HeroTag");

            // Обратные изменения типов...
            migrationBuilder.AlterColumn<int>(
                name: "SteamAccountId",
                table: "Players",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Xpm",
                table: "MatchPlayers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "MatchPlayers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "MatchPlayers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gpm",
                table: "MatchPlayers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            // Восстановление времени (int → DateTime)
            migrationBuilder.Sql(@"
                ALTER TABLE ""ItemPurchases"" ADD COLUMN ""OldPurchaseTime"" timestamp with time zone;
                UPDATE ""ItemPurchases"" SET ""OldPurchaseTime"" = to_timestamp(""PurchaseTime"");
                ALTER TABLE ""ItemPurchases"" DROP COLUMN ""PurchaseTime"";
                ALTER TABLE ""ItemPurchases"" RENAME COLUMN ""OldPurchaseTime"" TO ""PurchaseTime"";
                ALTER TABLE ""ItemPurchases"" ALTER COLUMN ""PurchaseTime"" SET NOT NULL;
            ");

            // Обратные изменения для Heroes
            migrationBuilder.AlterColumn<string>(
                name: "TalentTree",
                table: "Heroes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StrengthIncrease",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Strength",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Mana",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IntelligenceIncrease",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Intelligence",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Health",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackSpeed",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackRange",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttackInterval",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Armor",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgilityIncrease",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Agility",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_MatchId",
                table: "Heroes",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Matches_MatchId",
                table: "Heroes",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayers_Players_PlayerId",
                table: "MatchPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}