using Microsoft.EntityFrameworkCore.Migrations;

namespace EnglishApp.Migrations
{
    public partial class AddColumnToTypeLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "From",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Lessons",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "To",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "From", "Time", "To" },
                values: new object[] { 2, "8h-10h", 7 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "From", "Time", "To" },
                values: new object[] { 2, "10h-12h", 7 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "From", "Time", "To" },
                values: new object[] { 2, "13h-15h", 7 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "From", "Time", "To" },
                values: new object[] { 2, "15h-17h", 7 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "From", "Time", "To" },
                values: new object[] { 2, "18h-20h", 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Lessons");
        }
    }
}
