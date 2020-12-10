using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationSystem.Scheduling.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Scheduling");

            migrationBuilder.EnsureSchema(
                name: "Outbox");

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "Outbox",
                columns: table => new
                {
                    OutboxMessageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<Guid>(nullable: false),
                    ObjectId = table.Column<string>(nullable: true),
                    SerializedMessage = table.Column<string>(nullable: false),
                    OccurredOn = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    SentDate = table.Column<DateTime>(nullable: true),
                    AssemblyName = table.Column<string>(nullable: false),
                    FullNameMessageType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.OutboxMessageId);
                });

            migrationBuilder.CreateTable(
                name: "DailySchedules",
                schema: "Scheduling",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ServiceId = table.Column<long>(nullable: false),
                    Year = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Day = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySchedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledDates",
                schema: "Scheduling",
                columns: table => new
                {
                    DateId = table.Column<Guid>(nullable: false),
                    ScheduleId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: true),
                    EndTime = table.Column<TimeSpan>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledDates", x => x.DateId);
                    table.ForeignKey(
                        name: "FK_ScheduledDates_DailySchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "Scheduling",
                        principalTable: "DailySchedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDates_ScheduleId",
                schema: "Scheduling",
                table: "ScheduledDates",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Outbox");

            migrationBuilder.DropTable(
                name: "ScheduledDates",
                schema: "Scheduling");

            migrationBuilder.DropTable(
                name: "DailySchedules",
                schema: "Scheduling");
        }
    }
}
