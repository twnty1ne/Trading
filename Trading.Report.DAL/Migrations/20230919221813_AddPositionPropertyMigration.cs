using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trading.Report.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionPropertyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "Positions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Positions");
        }
    }
}
