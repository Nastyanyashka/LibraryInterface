using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class para : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompositionsAndPublishers_PublishingHouses_PublishingHouseId",
                table: "CompositionsAndPublishers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacesOfPublications_PublishingHouses_PublishingHouseId",
                table: "PlacesOfPublications");

            migrationBuilder.DropTable(
                name: "PublishingHouses");

            migrationBuilder.DropIndex(
                name: "IX_CompositionsAndPublishers_PublishingHouseId",
                table: "CompositionsAndPublishers");

            migrationBuilder.DropColumn(
                name: "PublishingHouseId",
                table: "CompositionsAndPublishers");

            migrationBuilder.RenameColumn(
                name: "PublishingHouseId",
                table: "PlacesOfPublications",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_PlacesOfPublications_PublishingHouseId",
                table: "PlacesOfPublications",
                newName: "IX_PlacesOfPublications_CityId");

            migrationBuilder.AddColumn<int>(
                name: "DaysOfIssuance",
                table: "TypesOfComposition",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PermissionOnIssuance",
                table: "TypesOfComposition",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlacesOfPublications_Cities_CityId",
                table: "PlacesOfPublications",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlacesOfPublications_Cities_CityId",
                table: "PlacesOfPublications");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropColumn(
                name: "DaysOfIssuance",
                table: "TypesOfComposition");

            migrationBuilder.DropColumn(
                name: "PermissionOnIssuance",
                table: "TypesOfComposition");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "PlacesOfPublications",
                newName: "PublishingHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_PlacesOfPublications_CityId",
                table: "PlacesOfPublications",
                newName: "IX_PlacesOfPublications_PublishingHouseId");

            migrationBuilder.AddColumn<int>(
                name: "PublishingHouseId",
                table: "CompositionsAndPublishers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PublishingHouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DaysOfIssuance = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionOnIssuance = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompositionsAndPublishers_PublishingHouseId",
                table: "CompositionsAndPublishers",
                column: "PublishingHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompositionsAndPublishers_PublishingHouses_PublishingHouseId",
                table: "CompositionsAndPublishers",
                column: "PublishingHouseId",
                principalTable: "PublishingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlacesOfPublications_PublishingHouses_PublishingHouseId",
                table: "PlacesOfPublications",
                column: "PublishingHouseId",
                principalTable: "PublishingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
