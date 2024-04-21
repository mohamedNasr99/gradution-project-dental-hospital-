using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalHospital.Migrations
{
    public partial class updatereport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clinic",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "Clinic",
                table: "MedicalReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "MedicalReports",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clinic",
                table: "MedicalReports");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "MedicalReports");

            migrationBuilder.AddColumn<string>(
                name: "Clinic",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
