using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cristalyn.Migrations
{
    public partial class AdaugareCupoane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupoane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Valoare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EsteProcentual = table.Column<bool>(type: "bit", nullable: false),
                    ValidDeLa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidPanaLa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumarMaximUtilizari = table.Column<int>(type: "int", nullable: true),
                    UtilizariCurente = table.Column<int>(type: "int", nullable: false),
                    ValoareMinimaComanda = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupoane", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "CuponId",
                table: "Comenzi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Reducere",
                table: "Comenzi",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_CuponId",
                table: "Comenzi",
                column: "CuponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comenzi_Cupoane_CuponId",
                table: "Comenzi",
                column: "CuponId",
                principalTable: "Cupoane",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comenzi_Cupoane_CuponId",
                table: "Comenzi");

            migrationBuilder.DropTable(
                name: "Cupoane");

            migrationBuilder.DropIndex(
                name: "IX_Comenzi_CuponId",
                table: "Comenzi");

            migrationBuilder.DropColumn(
                name: "CuponId",
                table: "Comenzi");

            migrationBuilder.DropColumn(
                name: "Reducere",
                table: "Comenzi");
        }
    }
} 