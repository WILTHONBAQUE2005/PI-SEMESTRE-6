using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimRoomWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VentasDiferidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClienteNombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MontoCompra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NumCuotas = table.Column<int>(type: "int", nullable: false),
                    IngresoMensual = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DiasAtrasoMax = table.Column<int>(type: "int", nullable: false),
                    PorcentajeCuotasPagadas = table.Column<double>(type: "double", nullable: false),
                    ProbabilidadMora = table.Column<double>(type: "double", nullable: false),
                    NivelRiesgo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasDiferidas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentasDiferidas");
        }
    }
}
