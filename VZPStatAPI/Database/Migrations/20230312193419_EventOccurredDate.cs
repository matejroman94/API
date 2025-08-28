using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class EventOccurredDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Printer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "DiagnosticBranch",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Counter",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Client",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "ClerkEvent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Clerk",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Activity",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Printer");

            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "DiagnosticBranch");

            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Counter");

            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "ClerkEvent");

            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Clerk");

            migrationBuilder.DropColumn(
                name: "EventOccurredDate",
                schema: "statistics",
                table: "Activity");
        }
    }
}
