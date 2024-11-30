using Microsoft.EntityFrameworkCore.Migrations;

namespace EnglishApp.Migrations
{
    public partial class UpdateDatatypeAccountStudentTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Students",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "From", "Name", "Time", "To" },
                values: new object[] { 5, 2, "Tối: 18h-20h từ T2-CN", "18h-20h", 8 });
        }
    }
}
