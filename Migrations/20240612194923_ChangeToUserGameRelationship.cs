using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameCollectionTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToUserGameRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_OwnerUserID",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "OwnerUserID",
                table: "Games",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Games_OwnerUserID",
                table: "Games",
                newName: "IX_Games_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Games",
                newName: "OwnerUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Games_UserID",
                table: "Games",
                newName: "IX_Games_OwnerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_OwnerUserID",
                table: "Games",
                column: "OwnerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
