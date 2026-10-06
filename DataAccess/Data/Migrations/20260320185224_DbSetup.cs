using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hostels",
                columns: table => new
                {
                    HostelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardenPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostels", x => x.HostelId);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProofNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.VisitorId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostelId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Hostels_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostels",
                        principalColumn: "HostelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staffs_Hostels_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostels",
                        principalColumn: "HostelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Girls",
                columns: table => new
                {
                    GirlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeavingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Girls", x => x.GirlId);
                    table.ForeignKey(
                        name: "FK_Girls_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryLogs",
                columns: table => new
                {
                    EntryLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GirlId = table.Column<int>(type: "int", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryLogs", x => x.EntryLogId);
                    table.ForeignKey(
                        name: "FK_EntryLogs_Girls_GirlId",
                        column: x => x.GirlId,
                        principalTable: "Girls",
                        principalColumn: "GirlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodAttendances",
                columns: table => new
                {
                    FoodAttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GirlId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAttendances", x => x.FoodAttendanceId);
                    table.ForeignKey(
                        name: "FK_FoodAttendances_Girls_GirlId",
                        column: x => x.GirlId,
                        principalTable: "Girls",
                        principalColumn: "GirlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostelAttendances",
                columns: table => new
                {
                    HostelAttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GirlId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelAttendances", x => x.HostelAttendanceId);
                    table.ForeignKey(
                        name: "FK_HostelAttendances_Girls_GirlId",
                        column: x => x.GirlId,
                        principalTable: "Girls",
                        principalColumn: "GirlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketVisits",
                columns: table => new
                {
                    MarketVisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GirlId = table.Column<int>(type: "int", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketVisits", x => x.MarketVisitId);
                    table.ForeignKey(
                        name: "FK_MarketVisits_Girls_GirlId",
                        column: x => x.GirlId,
                        principalTable: "Girls",
                        principalColumn: "GirlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomHistories",
                columns: table => new
                {
                    RoomHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GirlId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomHistories", x => x.RoomHistoryId);
                    table.ForeignKey(
                        name: "FK_RoomHistories_Girls_GirlId",
                        column: x => x.GirlId,
                        principalTable: "Girls",
                        principalColumn: "GirlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomHistories_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitorEntryLogs",
                columns: table => new
                {
                    VisitorEntryLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<int>(type: "int", nullable: false),
                    GirlId = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorEntryLogs", x => x.VisitorEntryLogId);
                    table.ForeignKey(
                        name: "FK_VisitorEntryLogs_Girls_GirlId",
                        column: x => x.GirlId,
                        principalTable: "Girls",
                        principalColumn: "GirlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitorEntryLogs_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryLogs_GirlId_OutTime",
                table: "EntryLogs",
                columns: new[] { "GirlId", "OutTime" });

            migrationBuilder.CreateIndex(
                name: "IX_FoodAttendances_GirlId_Date_MealType",
                table: "FoodAttendances",
                columns: new[] { "GirlId", "Date", "MealType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Girls_RoomId",
                table: "Girls",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HostelAttendances_GirlId_Date",
                table: "HostelAttendances",
                columns: new[] { "GirlId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketVisits_GirlId_OutTime",
                table: "MarketVisits",
                columns: new[] { "GirlId", "OutTime" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomHistories_GirlId",
                table: "RoomHistories",
                column: "GirlId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomHistories_RoomId",
                table: "RoomHistories",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostelId_RoomNumber",
                table: "Rooms",
                columns: new[] { "HostelId", "RoomNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_HostelId",
                table: "Staffs",
                column: "HostelId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Phone",
                table: "Staffs",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitorEntryLogs_GirlId",
                table: "VisitorEntryLogs",
                column: "GirlId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorEntryLogs_VisitorId",
                table: "VisitorEntryLogs",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_Phone",
                table: "Visitors",
                column: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryLogs");

            migrationBuilder.DropTable(
                name: "FoodAttendances");

            migrationBuilder.DropTable(
                name: "HostelAttendances");

            migrationBuilder.DropTable(
                name: "MarketVisits");

            migrationBuilder.DropTable(
                name: "RoomHistories");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "VisitorEntryLogs");

            migrationBuilder.DropTable(
                name: "Girls");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hostels");
        }
    }
}
