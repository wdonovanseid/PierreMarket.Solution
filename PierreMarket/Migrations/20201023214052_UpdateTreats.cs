using Microsoft.EntityFrameworkCore.Migrations;

namespace PierreMarket.Migrations
{
    public partial class UpdateTreats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TreatCost",
                table: "Treats",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OrderTotalCost",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatCost",
                table: "Treats");

            migrationBuilder.DropColumn(
                name: "OrderTotalCost",
                table: "Orders");
        }
    }
}
