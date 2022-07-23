using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ChangeRelationshipbetweenHekimAndAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HekimDetails_AspNetUsers_AppUserId",
                table: "HekimDetails");

            migrationBuilder.DropIndex(
                name: "IX_HekimDetails_AppUserId",
                table: "HekimDetails");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "HekimDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "HekimDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HekimDetails_AppUserId",
                table: "HekimDetails",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HekimDetails_AspNetUsers_AppUserId",
                table: "HekimDetails",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
