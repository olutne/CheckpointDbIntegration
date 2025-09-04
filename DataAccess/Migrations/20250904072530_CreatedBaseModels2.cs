using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBaseModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Trip_CostNonNegative",
                table: "trips");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Trip_DistanceNonNegative",
                table: "trips");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Trip_CostNonNegative",
                table: "trips",
                sql: "\"cost\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Trip_DistanceNonNegative",
                table: "trips",
                sql: "\"distance\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Trip_CostNonNegative",
                table: "trips");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Trip_DistanceNonNegative",
                table: "trips");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Trip_CostNonNegative",
                table: "trips",
                sql: "\"cost\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Trip_DistanceNonNegative",
                table: "trips",
                sql: "\"distance\" >= 0");
        }
    }
}
