using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class dolg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "TypesOfComposition",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfComposition_StorageId",
                table: "TypesOfComposition",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypesOfComposition_Storages_StorageId",
                table: "TypesOfComposition",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypesOfComposition_Storages_StorageId",
                table: "TypesOfComposition");

            migrationBuilder.DropIndex(
                name: "IX_TypesOfComposition_StorageId",
                table: "TypesOfComposition");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "TypesOfComposition");
        }
    }
}
