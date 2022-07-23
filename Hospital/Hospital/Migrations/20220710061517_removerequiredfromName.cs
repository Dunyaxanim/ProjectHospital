using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class removerequiredfromName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HekimDetails_AspNetUsers_AppUserId1",
                table: "HekimDetails");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "TimeSolts");

            migrationBuilder.DropIndex(
                name: "IX_HekimDetails_AppUserId1",
                table: "HekimDetails");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "HekimDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hekims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "HekimDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HekimDetails_AspNetUsers_AppUserId",
                table: "HekimDetails");

            migrationBuilder.DropIndex(
                name: "IX_HekimDetails_AppUserId",
                table: "HekimDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hekims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "HekimDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "HekimDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSolts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false),
                    Solt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoltName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSolts", x => x.Id);
                });

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
        }
    }
}
