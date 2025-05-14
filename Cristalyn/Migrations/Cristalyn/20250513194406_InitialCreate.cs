using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cristalyn.Migrations.Cristalyn
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CosItems_Comenzi_ComandaId",
                table: "CosItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CosItems",
                table: "CosItems");

            migrationBuilder.RenameTable(
                name: "CosItems",
                newName: "CosItem");

            migrationBuilder.RenameIndex(
                name: "IX_CosItems_ComandaId",
                table: "CosItem",
                newName: "IX_CosItem_ComandaId");

            migrationBuilder.AlterColumn<string>(
                name: "EmailUtilizator",
                table: "Comenzi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Comenzi",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "CosItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CosItem",
                table: "CosItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Utilizatori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizatori", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CosItem_Comenzi_ComandaId",
                table: "CosItem",
                column: "ComandaId",
                principalTable: "Comenzi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CosItem_Comenzi_ComandaId",
                table: "CosItem");

            migrationBuilder.DropTable(
                name: "Utilizatori");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CosItem",
                table: "CosItem");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Comenzi");

            migrationBuilder.RenameTable(
                name: "CosItem",
                newName: "CosItems");

            migrationBuilder.RenameIndex(
                name: "IX_CosItem_ComandaId",
                table: "CosItems",
                newName: "IX_CosItems_ComandaId");

            migrationBuilder.AlterColumn<string>(
                name: "EmailUtilizator",
                table: "Comenzi",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "CosItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CosItems",
                table: "CosItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CosItems_Comenzi_ComandaId",
                table: "CosItems",
                column: "ComandaId",
                principalTable: "Comenzi",
                principalColumn: "Id");
        }
    }
}
