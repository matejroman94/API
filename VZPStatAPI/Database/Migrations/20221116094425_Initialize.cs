using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "statistics");

            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "App",
                schema: "app",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowBranches = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "ClerkStatus",
                schema: "statistics",
                columns: table => new
                {
                    ClerkStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClerkStatus", x => x.ClerkStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ClientStatus",
                schema: "statistics",
                columns: table => new
                {
                    ClientStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientStatus", x => x.ClientStatusId);
                });

            migrationBuilder.CreateTable(
                name: "CounterStatus",
                schema: "statistics",
                columns: table => new
                {
                    CounterStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterStatus", x => x.CounterStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostic",
                schema: "statistics",
                columns: table => new
                {
                    DiagnosticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostic", x => x.DiagnosticId);
                });

            migrationBuilder.CreateTable(
                name: "EventName",
                schema: "statistics",
                columns: table => new
                {
                    EventNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventName", x => x.EventNameId);
                });

            migrationBuilder.CreateTable(
                name: "Logger",
                schema: "statistics",
                columns: table => new
                {
                    LoggerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logger", x => x.LoggerId);
                });

            migrationBuilder.CreateTable(
                name: "PacketDataType",
                schema: "statistics",
                columns: table => new
                {
                    ExtendedDataTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacketDataType", x => x.ExtendedDataTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PeriphType",
                schema: "statistics",
                columns: table => new
                {
                    PerihTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriphType", x => x.PerihTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PrinterCurrentStatus",
                schema: "statistics",
                columns: table => new
                {
                    PrinterCurrentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterCurrentStatus", x => x.PrinterCurrentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "PrinterPreviousStatus",
                schema: "statistics",
                columns: table => new
                {
                    PrinterPreviousStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterPreviousStatus", x => x.PrinterPreviousStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Reason",
                schema: "statistics",
                columns: table => new
                {
                    ReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reason", x => x.ReasonId);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "statistics",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "app",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "app",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Clerk",
                schema: "statistics",
                columns: table => new
                {
                    ClerkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CounterSignInId = table.Column<int>(type: "int", nullable: false),
                    ClerkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClerkStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clerk", x => x.ClerkId);
                    table.ForeignKey(
                        name: "FK_Clerk_ClerkStatus_ClerkStatusId",
                        column: x => x.ClerkStatusId,
                        principalSchema: "statistics",
                        principalTable: "ClerkStatus",
                        principalColumn: "ClerkStatusId");
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "statistics",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    VZP_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "statistics",
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                schema: "app",
                columns: table => new
                {
                    AppsAppId = table.Column<int>(type: "int", nullable: false),
                    RolesRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => new { x.AppsAppId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_AppRole_App_AppsAppId",
                        column: x => x.AppsAppId,
                        principalSchema: "app",
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRole_Role_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalSchema: "app",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                schema: "app",
                columns: table => new
                {
                    RolesRoleId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesRoleId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalSchema: "app",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalSchema: "app",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClerkEvent",
                schema: "statistics",
                columns: table => new
                {
                    ClerkEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClerkId = table.Column<int>(type: "int", nullable: false),
                    ClerkStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClerkEvent", x => x.ClerkEventId);
                    table.ForeignKey(
                        name: "FK_ClerkEvent_Clerk_ClerkId",
                        column: x => x.ClerkId,
                        principalSchema: "statistics",
                        principalTable: "Clerk",
                        principalColumn: "ClerkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClerkEvent_ClerkStatus_ClerkStatusId",
                        column: x => x.ClerkStatusId,
                        principalSchema: "statistics",
                        principalTable: "ClerkStatus",
                        principalColumn: "ClerkStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                schema: "statistics",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activity_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchUser",
                schema: "statistics",
                columns: table => new
                {
                    BranchesBranchId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchUser", x => new { x.BranchesBranchId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_BranchUser_Branch_BranchesBranchId",
                        column: x => x.BranchesBranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchUser_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalSchema: "app",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Counter",
                schema: "statistics",
                columns: table => new
                {
                    CounterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CounterStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CounterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClerkSignInId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counter", x => x.CounterId);
                    table.ForeignKey(
                        name: "FK_Counter_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Counter_CounterStatus_CounterStatusId",
                        column: x => x.CounterStatusId,
                        principalSchema: "statistics",
                        principalTable: "CounterStatus",
                        principalColumn: "CounterStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticBranch",
                schema: "statistics",
                columns: table => new
                {
                    DiagnosticBranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriphNumber = table.Column<int>(type: "int", nullable: true),
                    DiagnosticData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    DiagnosticId = table.Column<int>(type: "int", nullable: true),
                    PeriphTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticBranch", x => x.DiagnosticBranchId);
                    table.ForeignKey(
                        name: "FK_DiagnosticBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_DiagnosticBranch_Diagnostic_DiagnosticId",
                        column: x => x.DiagnosticId,
                        principalSchema: "statistics",
                        principalTable: "Diagnostic",
                        principalColumn: "DiagnosticId");
                    table.ForeignKey(
                        name: "FK_DiagnosticBranch_PeriphType_PeriphTypeId",
                        column: x => x.PeriphTypeId,
                        principalSchema: "statistics",
                        principalTable: "PeriphType",
                        principalColumn: "PerihTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "statistics",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    EventNameId = table.Column<int>(type: "int", nullable: false),
                    DiagnosticId = table.Column<int>(type: "int", nullable: true),
                    PrinterCurrentStateId = table.Column<int>(type: "int", nullable: true),
                    PrinterPreviousStateId = table.Column<int>(type: "int", nullable: true),
                    PeriphTypeId = table.Column<int>(type: "int", nullable: true),
                    PacketTypeId = table.Column<int>(type: "int", nullable: true),
                    ReasonId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventHour = table.Column<int>(type: "int", nullable: false),
                    EventMinute = table.Column<int>(type: "int", nullable: false),
                    EventSecond = table.Column<int>(type: "int", nullable: false),
                    EventHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientOrdinalNumber = table.Column<int>(type: "int", nullable: true),
                    Activity = table.Column<int>(type: "int", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: true),
                    PrinterNumber = table.Column<int>(type: "int", nullable: true),
                    NewActivity = table.Column<int>(type: "int", nullable: true),
                    NewCounter = table.Column<int>(type: "int", nullable: true),
                    NewClerk = table.Column<int>(type: "int", nullable: true),
                    EstimateWaiting = table.Column<int>(type: "int", nullable: true),
                    WaitingTime = table.Column<int>(type: "int", nullable: true),
                    ServiceWaiting = table.Column<int>(type: "int", nullable: true),
                    Clerk = table.Column<int>(type: "int", nullable: true),
                    ReasonSignout = table.Column<int>(type: "int", nullable: true),
                    DiagnosticPin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticPeriphTypeId = table.Column<int>(type: "int", nullable: true),
                    DiagnosticPeriphOrdinalNumber = table.Column<int>(type: "int", nullable: true),
                    DiagnosticData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacketNumOfBytes = table.Column<int>(type: "int", nullable: true),
                    PacketData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullyProcessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Diagnostic_DiagnosticId",
                        column: x => x.DiagnosticId,
                        principalSchema: "statistics",
                        principalTable: "Diagnostic",
                        principalColumn: "DiagnosticId");
                    table.ForeignKey(
                        name: "FK_Event_EventName_EventNameId",
                        column: x => x.EventNameId,
                        principalSchema: "statistics",
                        principalTable: "EventName",
                        principalColumn: "EventNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_PacketDataType_PacketTypeId",
                        column: x => x.PacketTypeId,
                        principalSchema: "statistics",
                        principalTable: "PacketDataType",
                        principalColumn: "ExtendedDataTypeId");
                    table.ForeignKey(
                        name: "FK_Event_PeriphType_PeriphTypeId",
                        column: x => x.PeriphTypeId,
                        principalSchema: "statistics",
                        principalTable: "PeriphType",
                        principalColumn: "PerihTypeId");
                    table.ForeignKey(
                        name: "FK_Event_PrinterCurrentStatus_PrinterCurrentStateId",
                        column: x => x.PrinterCurrentStateId,
                        principalSchema: "statistics",
                        principalTable: "PrinterCurrentStatus",
                        principalColumn: "PrinterCurrentStatusId");
                    table.ForeignKey(
                        name: "FK_Event_PrinterPreviousStatus_PrinterPreviousStateId",
                        column: x => x.PrinterPreviousStateId,
                        principalSchema: "statistics",
                        principalTable: "PrinterPreviousStatus",
                        principalColumn: "PrinterPreviousStatusId");
                    table.ForeignKey(
                        name: "FK_Event_Reason_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "statistics",
                        principalTable: "Reason",
                        principalColumn: "ReasonId");
                });

            migrationBuilder.CreateTable(
                name: "Printer",
                schema: "statistics",
                columns: table => new
                {
                    PrinterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    PrinterCurrentStateId = table.Column<int>(type: "int", nullable: false),
                    PrinterPreviousStateId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.PrinterId);
                    table.ForeignKey(
                        name: "FK_Printer_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Printer_PrinterCurrentStatus_PrinterCurrentStateId",
                        column: x => x.PrinterCurrentStateId,
                        principalSchema: "statistics",
                        principalTable: "PrinterCurrentStatus",
                        principalColumn: "PrinterCurrentStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Printer_PrinterPreviousStatus_PrinterPreviousStateId",
                        column: x => x.PrinterPreviousStateId,
                        principalSchema: "statistics",
                        principalTable: "PrinterPreviousStatus",
                        principalColumn: "PrinterPreviousStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClerkCounter",
                schema: "statistics",
                columns: table => new
                {
                    ClerksClerkId = table.Column<int>(type: "int", nullable: false),
                    CountersCounterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClerkCounter", x => new { x.ClerksClerkId, x.CountersCounterId });
                    table.ForeignKey(
                        name: "FK_ClerkCounter_Clerk_ClerksClerkId",
                        column: x => x.ClerksClerkId,
                        principalSchema: "statistics",
                        principalTable: "Clerk",
                        principalColumn: "ClerkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClerkCounter_Counter_CountersCounterId",
                        column: x => x.CountersCounterId,
                        principalSchema: "statistics",
                        principalTable: "Counter",
                        principalColumn: "CounterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "statistics",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ClerkId = table.Column<int>(type: "int", nullable: true),
                    ActivityId = table.Column<int>(type: "int", nullable: true),
                    PrinterId = table.Column<int>(type: "int", nullable: true),
                    CounterId = table.Column<int>(type: "int", nullable: true),
                    ClientStatusId = table.Column<int>(type: "int", nullable: true),
                    ClientDoneId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<bool>(type: "bit", nullable: false),
                    ClientOrdinalNumber = table.Column<int>(type: "int", nullable: false),
                    WaitingTime = table.Column<int>(type: "int", nullable: false),
                    ServiceWaiting = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Client_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "statistics",
                        principalTable: "Activity",
                        principalColumn: "ActivityId");
                    table.ForeignKey(
                        name: "FK_Client_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "statistics",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_Clerk_ClerkId",
                        column: x => x.ClerkId,
                        principalSchema: "statistics",
                        principalTable: "Clerk",
                        principalColumn: "ClerkId");
                    table.ForeignKey(
                        name: "FK_Client_ClientStatus_ClientStatusId",
                        column: x => x.ClientStatusId,
                        principalSchema: "statistics",
                        principalTable: "ClientStatus",
                        principalColumn: "ClientStatusId");
                    table.ForeignKey(
                        name: "FK_Client_Counter_CounterId",
                        column: x => x.CounterId,
                        principalSchema: "statistics",
                        principalTable: "Counter",
                        principalColumn: "CounterId");
                    table.ForeignKey(
                        name: "FK_Client_Printer_PrinterId",
                        column: x => x.PrinterId,
                        principalSchema: "statistics",
                        principalTable: "Printer",
                        principalColumn: "PrinterId");
                    table.ForeignKey(
                        name: "FK_Client_Reason_ClientDoneId",
                        column: x => x.ClientDoneId,
                        principalSchema: "statistics",
                        principalTable: "Reason",
                        principalColumn: "ReasonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_BranchId",
                schema: "statistics",
                table: "Activity",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_RolesRoleId",
                schema: "app",
                table: "AppRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_RegionId",
                schema: "statistics",
                table: "Branch",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchUser_UsersUserId",
                schema: "statistics",
                table: "BranchUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clerk_ClerkStatusId",
                schema: "statistics",
                table: "Clerk",
                column: "ClerkStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClerkCounter_CountersCounterId",
                schema: "statistics",
                table: "ClerkCounter",
                column: "CountersCounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClerkEvent_ClerkId",
                schema: "statistics",
                table: "ClerkEvent",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_ClerkEvent_ClerkStatusId",
                schema: "statistics",
                table: "ClerkEvent",
                column: "ClerkStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ActivityId",
                schema: "statistics",
                table: "Client",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_BranchId",
                schema: "statistics",
                table: "Client",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClerkId",
                schema: "statistics",
                table: "Client",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientDoneId",
                schema: "statistics",
                table: "Client",
                column: "ClientDoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientStatusId",
                schema: "statistics",
                table: "Client",
                column: "ClientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CounterId",
                schema: "statistics",
                table: "Client",
                column: "CounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_PrinterId",
                schema: "statistics",
                table: "Client",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_Counter_BranchId",
                schema: "statistics",
                table: "Counter",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Counter_CounterStatusId",
                schema: "statistics",
                table: "Counter",
                column: "CounterStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticBranch_BranchId",
                schema: "statistics",
                table: "DiagnosticBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticBranch_DiagnosticId",
                schema: "statistics",
                table: "DiagnosticBranch",
                column: "DiagnosticId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticBranch_PeriphTypeId",
                schema: "statistics",
                table: "DiagnosticBranch",
                column: "PeriphTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_BranchId",
                schema: "statistics",
                table: "Event",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_DiagnosticId",
                schema: "statistics",
                table: "Event",
                column: "DiagnosticId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventNameId",
                schema: "statistics",
                table: "Event",
                column: "EventNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PacketTypeId",
                schema: "statistics",
                table: "Event",
                column: "PacketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PeriphTypeId",
                schema: "statistics",
                table: "Event",
                column: "PeriphTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PrinterCurrentStateId",
                schema: "statistics",
                table: "Event",
                column: "PrinterCurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PrinterPreviousStateId",
                schema: "statistics",
                table: "Event",
                column: "PrinterPreviousStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ReasonId",
                schema: "statistics",
                table: "Event",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_BranchId",
                schema: "statistics",
                table: "Printer",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_PrinterCurrentStateId",
                schema: "statistics",
                table: "Printer",
                column: "PrinterCurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_PrinterPreviousStateId",
                schema: "statistics",
                table: "Printer",
                column: "PrinterPreviousStateId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersUserId",
                schema: "app",
                table: "RoleUser",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRole",
                schema: "app");

            migrationBuilder.DropTable(
                name: "BranchUser",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "ClerkCounter",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "ClerkEvent",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "DiagnosticBranch",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Logger",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "RoleUser",
                schema: "app");

            migrationBuilder.DropTable(
                name: "App",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Activity",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Clerk",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "ClientStatus",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Counter",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Printer",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Diagnostic",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "EventName",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "PacketDataType",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "PeriphType",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Reason",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "app");

            migrationBuilder.DropTable(
                name: "User",
                schema: "app");

            migrationBuilder.DropTable(
                name: "ClerkStatus",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "CounterStatus",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "PrinterCurrentStatus",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "PrinterPreviousStatus",
                schema: "statistics");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "statistics");
        }
    }
}
