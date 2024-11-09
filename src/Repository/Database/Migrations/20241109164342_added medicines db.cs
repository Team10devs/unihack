using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedmedicinesdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineModel_Prescriptions_PrescriptionModelId",
                table: "MedicineModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicineModel",
                table: "MedicineModel");

            migrationBuilder.RenameTable(
                name: "MedicineModel",
                newName: "Medicines");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineModel_PrescriptionModelId",
                table: "Medicines",
                newName: "IX_Medicines_PrescriptionModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Prescriptions_PrescriptionModelId",
                table: "Medicines",
                column: "PrescriptionModelId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Prescriptions_PrescriptionModelId",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines");

            migrationBuilder.RenameTable(
                name: "Medicines",
                newName: "MedicineModel");

            migrationBuilder.RenameIndex(
                name: "IX_Medicines_PrescriptionModelId",
                table: "MedicineModel",
                newName: "IX_MedicineModel_PrescriptionModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicineModel",
                table: "MedicineModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineModel_Prescriptions_PrescriptionModelId",
                table: "MedicineModel",
                column: "PrescriptionModelId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }
    }
}
