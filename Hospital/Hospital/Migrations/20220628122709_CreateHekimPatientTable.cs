using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class CreateHekimPatientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hekims_Patients_PatientId",
                table: "Hekims");

            migrationBuilder.DropIndex(
                name: "IX_Hekims_PatientId",
                table: "Hekims");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Hekims");

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
                name: "IX_HekimPatients_HekimId",
                table: "HekimPatients",
                column: "HekimId");

            migrationBuilder.CreateIndex(
                name: "IX_HekimPatients_PatientId",
                table: "HekimPatients",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HekimPatients");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Hekims",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hekims_PatientId",
                table: "Hekims",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hekims_Patients_PatientId",
                table: "Hekims",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
