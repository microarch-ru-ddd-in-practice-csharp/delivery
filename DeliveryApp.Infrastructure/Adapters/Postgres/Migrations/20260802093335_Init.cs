using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Infrastructure.Adapters.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_x = table.Column<int>(type: "integer", nullable: false),
                    location_y = table.Column<int>(type: "integer", nullable: false),
                    max_volume = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_x = table.Column<int>(type: "integer", nullable: false),
                    location_y = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    Status_Id = table.Column<int>(type: "integer", nullable: false),
                    volume = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    volume = table.Column<int>(type: "integer", nullable: false),
                    location_x = table.Column<int>(type: "integer", nullable: false),
                    location_y = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    Status_Id = table.Column<int>(type: "integer", nullable: false),
                    courierid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Assignments_Couriers_courierid",
                        column: x => x.courierid,
                        principalTable: "Couriers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_courierid",
                table: "Assignments",
                column: "courierid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Couriers");
        }
    }
}
