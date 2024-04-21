using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalHospital.Migrations
{
    public partial class updateclinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Clinics_ClinicId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_MedicalReports_MedicalReportId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Clinics_ClinicId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClinicId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "MedicalReportId",
                table: "Sessions",
                newName: "MedicalReportCode");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_MedicalReportId",
                table: "Sessions",
                newName: "IX_Sessions_MedicalReportCode");

            migrationBuilder.AddColumn<string>(
                name: "ClinicName",
                table: "Students",
                type: "nvarchar(70)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClinicId",
                table: "Professors",
                type: "nvarchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClinicName",
                table: "Students",
                column: "ClinicName");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Clinics_ClinicId",
                table: "Professors",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_MedicalReports_MedicalReportCode",
                table: "Sessions",
                column: "MedicalReportCode",
                principalTable: "MedicalReports",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Clinics_ClinicName",
                table: "Students",
                column: "ClinicName",
                principalTable: "Clinics",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Clinics_ClinicId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_MedicalReports_MedicalReportCode",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Clinics_ClinicName",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClinicName",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "ClinicName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "MedicalReportCode",
                table: "Sessions",
                newName: "MedicalReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_MedicalReportCode",
                table: "Sessions",
                newName: "IX_Sessions_MedicalReportId");

            migrationBuilder.AddColumn<string>(
                name: "ClinicId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClinicId",
                table: "Professors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Clinics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clinics",
                table: "Clinics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClinicId",
                table: "Students",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Clinics_ClinicId",
                table: "Professors",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_MedicalReports_MedicalReportId",
                table: "Sessions",
                column: "MedicalReportId",
                principalTable: "MedicalReports",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Clinics_ClinicId",
                table: "Students",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
