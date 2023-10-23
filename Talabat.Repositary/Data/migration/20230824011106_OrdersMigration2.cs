using Microsoft.EntityFrameworkCore.Migrations;

namespace Talabat.Repositary.Data.migration
{
    public partial class OrdersMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "shortName",
                table: "DelivaryMethods",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "Descrpition",
                table: "DelivaryMethods",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DelivaryTime",
                table: "DelivaryMethods",
                newName: "DeliveryTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "DelivaryMethods",
                newName: "shortName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DelivaryMethods",
                newName: "Descrpition");

            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DelivaryMethods",
                newName: "DelivaryTime");
        }
    }
}
