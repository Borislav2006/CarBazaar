using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Migrations
{
    /// <inheritdoc />
    public partial class fuel_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Listings",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Listings");
        }
    }
}
