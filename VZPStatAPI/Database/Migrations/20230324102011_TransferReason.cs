using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class TransferReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferReasonId",
                schema: "statistics",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferReasonId",
                schema: "statistics",
                table: "Client",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransferReason",
                schema: "statistics",
                columns: table => new
                {
                    TransferReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferReason", x => x.TransferReasonId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_TransferReasonId",
                schema: "statistics",
                table: "Event",
                column: "TransferReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_TransferReasonId",
                schema: "statistics",
                table: "Client",
                column: "TransferReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_TransferReason_TransferReasonId",
                schema: "statistics",
                table: "Client",
                column: "TransferReasonId",
                principalSchema: "statistics",
                principalTable: "TransferReason",
                principalColumn: "TransferReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_TransferReason_TransferReasonId",
                schema: "statistics",
                table: "Event",
                column: "TransferReasonId",
                principalSchema: "statistics",
                principalTable: "TransferReason",
                principalColumn: "TransferReasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_TransferReason_TransferReasonId",
                schema: "statistics",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_TransferReason_TransferReasonId",
                schema: "statistics",
                table: "Event");

            migrationBuilder.DropTable(
                name: "TransferReason",
                schema: "statistics");

            migrationBuilder.DropIndex(
                name: "IX_Event_TransferReasonId",
                schema: "statistics",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Client_TransferReasonId",
                schema: "statistics",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "TransferReasonId",
                schema: "statistics",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "TransferReasonId",
                schema: "statistics",
                table: "Client");
        }
    }
}
