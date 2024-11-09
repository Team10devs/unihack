using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAPI.Migrations
{
    /// <inheritdoc />
    public partial class prescriptionsandpdfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "MedicineModel");

            migrationBuilder.AddColumn<string>(
                name: "DoctorModelId",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientModelId",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfId",
                table: "Prescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Patients",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "MedicineModel",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "MedicineModel",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PrescriptionPdfs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    PrescriptionId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionPdfs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorModelId",
                table: "Prescriptions",
                column: "DoctorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientModelId",
                table: "Prescriptions",
                column: "PatientModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PdfId",
                table: "Prescriptions",
                column: "PdfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorModelId",
                table: "Prescriptions",
                column: "DoctorModelId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_PatientModelId",
                table: "Prescriptions",
                column: "PatientModelId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_PrescriptionPdfs_PdfId",
                table: "Prescriptions",
                column: "PdfId",
                principalTable: "PrescriptionPdfs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorModelId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_PatientModelId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_PrescriptionPdfs_PdfId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PrescriptionPdfs");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorModelId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PatientModelId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PdfId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorModelId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PatientModelId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PdfId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "MedicineModel");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "MedicineModel");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Prescriptions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Prescriptions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Patients",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "MedicineModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorId",
                table: "Patients",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
