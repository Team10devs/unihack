using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAPI.Migrations
{
    /// <inheritdoc />
    public partial class changedappointmentendfieldfromtimespantodatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDuration",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "AppointmentStartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentEndTime",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentEndTime",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentStartTime",
                table: "Appointments",
                newName: "AppointmentDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AppointmentDuration",
                table: "Appointments",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
