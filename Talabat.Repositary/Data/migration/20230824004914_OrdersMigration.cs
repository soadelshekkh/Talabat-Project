using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Talabat.Repositary.Data.migration
{
    public partial class OrdersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productBrands_ProductbrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_productTypeId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "productTypeId",
                table: "products",
                newName: "ProductTypeId");

            migrationBuilder.RenameColumn(
                name: "ProductbrandId",
                table: "products",
                newName: "ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_products_productTypeId",
                table: "products",
                newName: "IX_products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_ProductbrandId",
                table: "products",
                newName: "IX_products_ProductBrandId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "productBrands",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "DelivaryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    shortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelivaryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descrpition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelivaryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress_Countary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delivarymethodId = table.Column<int>(type: "int", nullable: true),
                    PaymentIntedId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DelivaryMethods_delivarymethodId",
                        column: x => x.delivarymethodId,
                        principalTable: "DelivaryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductItemOrdered_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductItemOrdered_ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductItemOrdered_ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_delivarymethodId",
                table: "Orders",
                column: "delivarymethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productBrands_ProductBrandId",
                table: "products",
                column: "ProductBrandId",
                principalTable: "productBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_ProductTypeId",
                table: "products",
                column: "ProductTypeId",
                principalTable: "productTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productBrands_ProductBrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_ProductTypeId",
                table: "products");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DelivaryMethods");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "products",
                newName: "productTypeId");

            migrationBuilder.RenameColumn(
                name: "ProductBrandId",
                table: "products",
                newName: "ProductbrandId");

            migrationBuilder.RenameIndex(
                name: "IX_products_ProductTypeId",
                table: "products",
                newName: "IX_products_productTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_ProductBrandId",
                table: "products",
                newName: "IX_products_ProductbrandId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "productBrands",
                newName: "name");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productBrands_ProductbrandId",
                table: "products",
                column: "ProductbrandId",
                principalTable: "productBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_productTypeId",
                table: "products",
                column: "productTypeId",
                principalTable: "productTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
