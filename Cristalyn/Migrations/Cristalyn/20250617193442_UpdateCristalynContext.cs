using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cristalyn.Migrations.Cristalyn
{
    /// <inheritdoc />
    public partial class UpdateCristalynContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Parola",
                table: "Utilizatori",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Utilizatori",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Produse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Produse",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Categorie",
                table: "Produse",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateTable(
                name: "PuncteFidelitate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailUtilizator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PuncteAcumulate = table.Column<int>(type: "int", nullable: false),
                    PuncteFolosite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuncteFidelitate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReduceriCategorii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categorie = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcentReducere = table.Column<int>(type: "int", nullable: false),
                    ValidDeLa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidPanaLa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReduceriCategorii", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_CuponId",
                table: "Comenzi",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_PuncteFidelitate_EmailUtilizator",
                table: "PuncteFidelitate",
                column: "EmailUtilizator",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReduceriCategorii_Categorie",
                table: "ReduceriCategorii",
                column: "Categorie");

            migrationBuilder.AddForeignKey(
                name: "FK_Comenzi_Cupoane_CuponId",
                table: "Comenzi",
                column: "CuponId",
                principalTable: "Cupoane",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comenzi_Cupoane_CuponId",
                table: "Comenzi");

            migrationBuilder.DropTable(
                name: "Cupoane");

            migrationBuilder.DropTable(
                name: "PuncteFidelitate");

            migrationBuilder.DropTable(
                name: "ReduceriCategorii");

            migrationBuilder.DropIndex(
                name: "IX_Comenzi_CuponId",
                table: "Comenzi");

            migrationBuilder.DropColumn(
                name: "CuponId",
                table: "Comenzi");

            migrationBuilder.DropColumn(
                name: "Reducere",
                table: "Comenzi");

            migrationBuilder.AlterColumn<string>(
                name: "Parola",
                table: "Utilizatori",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Utilizatori",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Produse",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Produse",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Categorie",
                table: "Produse",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
