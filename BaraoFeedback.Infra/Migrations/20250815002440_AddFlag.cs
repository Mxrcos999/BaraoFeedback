using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaraoFeedback.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReceiveEmails",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveEmails",
                table: "AspNetUsers");
        }
    }
}
