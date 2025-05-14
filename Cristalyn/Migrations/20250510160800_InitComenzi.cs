using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cristalyn.Migrations
{
    /// <inheritdoc />
    public partial class InitComenzi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comenzi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comenzi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CosItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdusId = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CosItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CosItems_Comenzi_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comenzi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CosItems_ComandaId",
                table: "CosItems",
                column: "ComandaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CosItems");

            migrationBuilder.DropTable(
                name: "Comenzi");
        }
    }
}
