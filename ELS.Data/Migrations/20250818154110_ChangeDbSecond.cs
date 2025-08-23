using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDbSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialCost",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UsedParts",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ReportNumber",
                table: "Reports",
                newName: "ReportSerialNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportSerialNumber",
                table: "Reports",
                newName: "ReportNumber");

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialCost",
                table: "Reports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UsedParts",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
