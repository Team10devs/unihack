using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAPI.Migrations
{
    /// <inheritdoc />
    public partial class addeddevicetoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceToken",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceToken",
                table: "Doctors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "Doctors");
        }
    }
}
