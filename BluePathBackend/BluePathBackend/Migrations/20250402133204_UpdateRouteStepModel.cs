using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluePathBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRouteStepModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "x",
                table: "RouteSteps",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "y",
                table: "RouteSteps",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "x",
                table: "RouteSteps");

            migrationBuilder.DropColumn(
                name: "y",
                table: "RouteSteps");
        }
    }
}
