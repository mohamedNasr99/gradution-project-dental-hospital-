using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalHospital.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_Clinics_ClinicId",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_Patients_PatientSSN",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReports_ClinicId",
                table: "MedicalReports");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "MedicalReports");

            migrationBuilder.RenameColumn(
                name: "PatientSSN",
                table: "MedicalReports",
                newName: "PatientCode");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalReports_PatientSSN",
                table: "MedicalReports",
                newName: "IX_MedicalReports_PatientCode");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SSN",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissibleCases = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_Patients_PatientCode",
                table: "MedicalReports",
                column: "PatientCode",
                principalTable: "Patients",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_Patients_PatientCode",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "PatientCode",
                table: "MedicalReports",
                newName: "PatientSSN");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalReports_PatientCode",
                table: "MedicalReports",
                newName: "IX_MedicalReports_PatientSSN");

            migrationBuilder.AlterColumn<string>(
                name: "SSN",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ClinicId",
                table: "MedicalReports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "SSN");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_ClinicId",
                table: "MedicalReports",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_Clinics_ClinicId",
                table: "MedicalReports",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_Patients_PatientSSN",
                table: "MedicalReports",
                column: "PatientSSN",
                principalTable: "Patients",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Clinics_ClinicId",
                table: "Patients",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
