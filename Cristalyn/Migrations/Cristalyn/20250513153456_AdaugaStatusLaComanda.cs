using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cristalyn.Migrations.Cristalyn
{
    /// <inheritdoc />
    public partial class AdaugaStatusLaComanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Comenzi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comenzi");
        }
    }
}
