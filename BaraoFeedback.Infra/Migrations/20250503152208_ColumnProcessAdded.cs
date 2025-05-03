using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaraoFeedback.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ColumnProcessAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "Ticket",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Processed",
                table: "Ticket");
        }
    }
}
