using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prototipo1.Migrations
{
    /// <inheritdoc />
    public partial class TiemposReporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaAceptado",
                table: "Reportes",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaCancelacion",
                table: "Reportes",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaCreacion",
                table: "Reportes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaFinalizacion",
                table: "Reportes",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraAceptado",
                table: "Reportes",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraCancelacion",
                table: "Reportes",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraCrecacion",
                table: "Reportes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraFinalizacion",
                table: "Reportes",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAceptado",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FechaCancelacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "FechaFinalizacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "HoraAceptado",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "HoraCancelacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "HoraCrecacion",
                table: "Reportes");

            migrationBuilder.DropColumn(
                name: "HoraFinalizacion",
                table: "Reportes");
        }
    }
}
