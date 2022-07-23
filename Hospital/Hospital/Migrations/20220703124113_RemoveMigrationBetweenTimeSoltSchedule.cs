using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class RemoveMigrationBetweenTimeSoltSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_TimeSolts_TimeSoltId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TimeSoltId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TimeSoltId",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSoltId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TimeSoltId",
                table: "Schedules",
                column: "TimeSoltId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_TimeSolts_TimeSoltId",
                table: "Schedules",
                column: "TimeSoltId",
                principalTable: "TimeSolts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
