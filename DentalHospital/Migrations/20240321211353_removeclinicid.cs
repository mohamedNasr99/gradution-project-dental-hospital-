using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalHospital.Migrations
{
    public partial class removeclinicid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "Clinic",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clinic",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "IdentityNumber",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
