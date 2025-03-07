using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaraoFeedback.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Ticket",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_LocationId",
                table: "Ticket",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Location_LocationId",
                table: "Ticket",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Location_LocationId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_LocationId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Ticket");
        }
    }
}
