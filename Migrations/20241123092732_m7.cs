using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ptoba_svoego_vhoda_reg_2.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bron_Nomer_NomerId",
                table: "Bron");

            migrationBuilder.DropForeignKey(
                name: "FK_Bron_User_UserId",
                table: "Bron");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bron",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NomerId",
                table: "Bron",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bron_Nomer_NomerId",
                table: "Bron",
                column: "NomerId",
                principalTable: "Nomer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bron_User_UserId",
                table: "Bron",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bron_Nomer_NomerId",
                table: "Bron");

            migrationBuilder.DropForeignKey(
                name: "FK_Bron_User_UserId",
                table: "Bron");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bron",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NomerId",
                table: "Bron",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bron_Nomer_NomerId",
                table: "Bron",
                column: "NomerId",
                principalTable: "Nomer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bron_User_UserId",
                table: "Bron",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
