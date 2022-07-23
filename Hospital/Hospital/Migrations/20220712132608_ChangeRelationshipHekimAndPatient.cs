using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ChangeRelationshipHekimAndPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hekims_AspNetUsers_AppUserId",
                table: "Hekims");

            migrationBuilder.DropTable(
                name: "HekimPatients");

            migrationBuilder.DropIndex(
                name: "IX_Hekims_AppUserId",
                table: "Hekims");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Hekims");

            migrationBuilder.AddColumn<int>(
                name: "HekimId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HekimId",
                table: "Patients",
                column: "HekimId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hekims_HekimId",
                table: "Patients",
                column: "HekimId",
                principalTable: "Hekims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hekims_HekimId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_HekimId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HekimId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Hekims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HekimPatients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HekimId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HekimPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HekimPatients_Hekims_HekimId",
                        column: x => x.HekimId,
                        principalTable: "Hekims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HekimPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hekims_AppUserId",
                table: "Hekims",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HekimPatients_HekimId",
                table: "HekimPatients",
                column: "HekimId");

            migrationBuilder.CreateIndex(
                name: "IX_HekimPatients_PatientId",
                table: "HekimPatients",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hekims_AspNetUsers_AppUserId",
                table: "Hekims",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
