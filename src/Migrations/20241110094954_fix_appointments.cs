using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAPI.Migrations
{
    /// <inheritdoc />
    public partial class fix_appointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_PatientModelId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PatientModelId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PatientModelId",
                table: "Prescriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions");

            migrationBuilder.AddColumn<string>(
                name: "PatientModelId",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientModelId",
                table: "Prescriptions",
                column: "PatientModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_PatientModelId",
                table: "Prescriptions",
                column: "PatientModelId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
