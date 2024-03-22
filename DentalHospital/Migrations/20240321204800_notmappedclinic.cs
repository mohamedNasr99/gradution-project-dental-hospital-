using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalHospital.Migrations
{
    public partial class notmappedclinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ClinicId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ClinicId",
                table: "Patients",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
