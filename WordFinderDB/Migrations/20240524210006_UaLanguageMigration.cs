using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordFinderDB.Migrations
{
    /// <inheritdoc />
    public partial class UaLanguageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameWordCategories_uk_UA_GameWordCategories_Id",
                table: "GameWordCategories_uk_UA");

            migrationBuilder.DropForeignKey(
                name: "FK_GameWords_uk_UA_GameWords_Id",
                table: "GameWords_uk_UA");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "GameWords_uk_UA",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Complexity",
                table: "GameWords_uk_UA",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GameWords_uk_UA",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswered",
                table: "GameWords_uk_UA",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlayed",
                table: "GameWords_uk_UA",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPro",
                table: "GameWords_uk_UA",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "GameWords_uk_UA",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GameWordCategories_uk_UA",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameWords_uk_UA_CategoryId",
                table: "GameWords_uk_UA",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameWords_uk_UA_GameWordCategories_uk_UA_CategoryId",
                table: "GameWords_uk_UA",
                column: "CategoryId",
                principalTable: "GameWordCategories_uk_UA",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameWords_uk_UA_GameWordCategories_uk_UA_CategoryId",
                table: "GameWords_uk_UA");

            migrationBuilder.DropIndex(
                name: "IX_GameWords_uk_UA_CategoryId",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "Complexity",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "IsAnswered",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "IsPlayed",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "IsPro",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "Word",
                table: "GameWords_uk_UA");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GameWordCategories_uk_UA");

            migrationBuilder.AddForeignKey(
                name: "FK_GameWordCategories_uk_UA_GameWordCategories_Id",
                table: "GameWordCategories_uk_UA",
                column: "Id",
                principalTable: "GameWordCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameWords_uk_UA_GameWords_Id",
                table: "GameWords_uk_UA",
                column: "Id",
                principalTable: "GameWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
