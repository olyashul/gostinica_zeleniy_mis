using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ptoba_svoego_vhoda_reg_2.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bron",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_zaezd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_viezd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stoimost = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bron", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bron_Nomer_NomerId",
                        column: x => x.NomerId,
                        principalTable: "Nomer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bron_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bron_NomerId",
                table: "Bron",
                column: "NomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bron_UserId",
                table: "Bron",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bron");

            migrationBuilder.DropTable(
                name: "Nomer");
        }
    }
}
