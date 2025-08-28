using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class ParentBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentBranchId",
                schema: "statistics",
                table: "Branch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_ParentBranchId",
                schema: "statistics",
                table: "Branch",
                column: "ParentBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Branch_ParentBranchId",
                schema: "statistics",
                table: "Branch",
                column: "ParentBranchId",
                principalSchema: "statistics",
                principalTable: "Branch",
                principalColumn: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Branch_ParentBranchId",
                schema: "statistics",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_ParentBranchId",
                schema: "statistics",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "ParentBranchId",
                schema: "statistics",
                table: "Branch");
        }
    }
}
