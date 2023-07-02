using Microsoft.EntityFrameworkCore.Migrations;

namespace Engzny.Migrations
{
    public partial class OrderMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderOrderStatus");

            migrationBuilder.CreateTable(
                name: "Order_Status",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Status", x => new { x.OrderId, x.StatusId });
                    table.ForeignKey(
                        name: "FK_Order_Status_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Status_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Status_StatusId",
                table: "Order_Status",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Status");

            migrationBuilder.CreateTable(
                name: "OrderOrderStatus",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    StatusesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOrderStatus", x => new { x.OrdersId, x.StatusesId });
                    table.ForeignKey(
                        name: "FK_OrderOrderStatus_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderOrderStatus_OrderStatuses_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderOrderStatus_StatusesId",
                table: "OrderOrderStatus",
                column: "StatusesId");
        }
    }
}
