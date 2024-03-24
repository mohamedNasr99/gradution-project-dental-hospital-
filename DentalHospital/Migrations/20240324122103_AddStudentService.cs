using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalHospital.Migrations
{
    public partial class AddStudentService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Professors");
        }
    }
}
