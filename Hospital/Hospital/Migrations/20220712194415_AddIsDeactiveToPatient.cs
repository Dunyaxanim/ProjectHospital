using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddIsDeactiveToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "PatientDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Patients");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "PatientDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
