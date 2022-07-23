using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class CreateRelationShipBewtweenAppUserAndHekimDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HekimDetails");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Hekims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "HekimDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "HekimDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hekims_AppUserId",
                table: "Hekims",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HekimDetails_AppUserId1",
                table: "HekimDetails",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HekimDetails_AspNetUsers_AppUserId1",
                table: "HekimDetails",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hekims_AspNetUsers_AppUserId",
                table: "Hekims",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HekimDetails_AspNetUsers_AppUserId1",
                table: "HekimDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hekims_AspNetUsers_AppUserId",
                table: "Hekims");

            migrationBuilder.DropIndex(
                name: "IX_Hekims_AppUserId",
                table: "Hekims");

            migrationBuilder.DropIndex(
                name: "IX_HekimDetails_AppUserId1",
                table: "HekimDetails");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Hekims");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "HekimDetails");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "HekimDetails");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HekimDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
