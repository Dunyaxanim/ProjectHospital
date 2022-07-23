using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class CreateTableTimeSoltCreateIncomeCategoryAndIncomeTablesCreateExpenseCategoryAndExpenseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Schedules_ScheduleId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hekims_Schedules_ScheduleId",
                table: "Hekims");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Schedules_ScheduleId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedules_Departments_DepartmentId",
                table: "RoomSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedules_Schedules_ScheduleId",
                table: "RoomSchedules");

            migrationBuilder.DropIndex(
                name: "IX_RoomSchedules_DepartmentId",
                table: "RoomSchedules");

            migrationBuilder.DropIndex(
                name: "IX_RoomSchedules_ScheduleId",
                table: "RoomSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ScheduleId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Hekims_ScheduleId",
                table: "Hekims");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ScheduleId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AvailableDay",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "PerPatientTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "RoomSchedules");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "RoomSchedules");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Hekims");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "SoltName",
                table: "Schedules",
                newName: "Days");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HekimId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeSoltId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSolts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoltName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSolts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_IncomeCategories_IncomeCategoryId",
                        column: x => x.IncomeCategoryId,
                        principalTable: "IncomeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_HekimId",
                table: "Schedules",
                column: "HekimId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TimeSoltId",
                table: "Schedules",
                column: "TimeSoltId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseCategoryId",
                table: "Expenses",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeCategoryId",
                table: "Incomes",
                column: "IncomeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Hekims_HekimId",
                table: "Schedules",
                column: "HekimId",
                principalTable: "Hekims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_TimeSolts_TimeSoltId",
                table: "Schedules",
                column: "TimeSoltId",
                principalTable: "TimeSolts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Hekims_HekimId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_TimeSolts_TimeSoltId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "TimeSolts");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "IncomeCategories");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_HekimId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TimeSoltId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "HekimId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TimeSoltId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "Schedules",
                newName: "SoltName");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "AvailableDay",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerPatientTime",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "RoomSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "RoomSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Hekims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedules_DepartmentId",
                table: "RoomSchedules",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedules_ScheduleId",
                table: "RoomSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ScheduleId",
                table: "Rooms",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Hekims_ScheduleId",
                table: "Hekims",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ScheduleId",
                table: "Departments",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Schedules_ScheduleId",
                table: "Departments",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hekims_Schedules_ScheduleId",
                table: "Hekims",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Schedules_ScheduleId",
                table: "Rooms",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedules_Departments_DepartmentId",
                table: "RoomSchedules",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedules_Schedules_ScheduleId",
                table: "RoomSchedules",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
