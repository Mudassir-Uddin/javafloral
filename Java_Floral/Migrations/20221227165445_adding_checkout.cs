using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Java_Floral.Migrations
{
    public partial class adding_checkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order_Informations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Company_Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Town_City = table.Column<string>(nullable: true),
                    Postal_Code = table.Column<string>(nullable: true),
                    Email_Address = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Informations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Payement_Methods",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Shipping_Chargers = table.Column<int>(nullable: false),
                    Sub_Total = table.Column<int>(nullable: false),
                    Grand_Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payement_Methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Order_Informationid = table.Column<int>(nullable: false),
                    Payement_Methodid = table.Column<int>(nullable: false),
                    idenityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Order_Informations_Order_Informationid",
                        column: x => x.Order_Informationid,
                        principalTable: "Order_Informations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payement_Methods_Payement_Methodid",
                        column: x => x.Payement_Methodid,
                        principalTable: "Payement_Methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_idenityUserId",
                        column: x => x.idenityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "checkOuts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orderid = table.Column<int>(nullable: true),
                    Productsid = table.Column<int>(nullable: true),
                    Total_Quantity = table.Column<int>(nullable: false),
                    Total_Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkOuts", x => x.id);
                    table.ForeignKey(
                        name: "FK_checkOuts_Orders_Orderid",
                        column: x => x.Orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkOuts_Products_Productsid",
                        column: x => x.Productsid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkOuts_Orderid",
                table: "checkOuts",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_checkOuts_Productsid",
                table: "checkOuts",
                column: "Productsid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order_Informationid",
                table: "Orders",
                column: "Order_Informationid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Payement_Methodid",
                table: "Orders",
                column: "Payement_Methodid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_idenityUserId",
                table: "Orders",
                column: "idenityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkOuts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Order_Informations");

            migrationBuilder.DropTable(
                name: "Payement_Methods");
        }
    }
}
