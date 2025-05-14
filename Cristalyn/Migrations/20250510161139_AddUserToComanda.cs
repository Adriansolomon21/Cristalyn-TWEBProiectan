using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cristalyn.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToComanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailUtilizator",
                table: "Comenzi",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailUtilizator",
                table: "Comenzi");
        }
    }
}
