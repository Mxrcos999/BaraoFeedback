using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaraoFeedback.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnisActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TicketCategory",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TicketCategory");
        }
    }
}
