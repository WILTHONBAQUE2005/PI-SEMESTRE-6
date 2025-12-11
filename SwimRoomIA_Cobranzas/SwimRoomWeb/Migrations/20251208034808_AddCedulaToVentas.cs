using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimRoomWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddCedulaToVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "VentasDiferidas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "VentasDiferidas");
        }
    }
}
