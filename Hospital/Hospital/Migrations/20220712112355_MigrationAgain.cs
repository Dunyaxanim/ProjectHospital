using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class MigrationAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "HekimDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Hekims",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Hekims");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "HekimDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
