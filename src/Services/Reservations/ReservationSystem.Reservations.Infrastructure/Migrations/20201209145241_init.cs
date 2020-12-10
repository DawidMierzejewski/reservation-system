using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationSystem.Reservations.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Outbox");

            migrationBuilder.EnsureSchema(
                name: "Reservations");

            migrationBuilder.EnsureSchema(
                name: "Services");

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
                name: "Reservations",
                schema: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ReservationStatus = table.Column<int>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanBeReserved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "ReservationDates",
                schema: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<long>(nullable: false),
                    DateId = table.Column<Guid>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDates", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_ReservationDates_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "Reservations",
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "Outbox");

            migrationBuilder.DropTable(
                name: "ReservationDates",
                schema: "Reservations");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "Services");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "Reservations");
        }
    }
}
