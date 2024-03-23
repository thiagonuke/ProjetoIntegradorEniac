using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Processo.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    senha = table.Column<string>(type: "TEXT", nullable: false),
                    Grupo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadastros", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cadastros");
        }
    }
}
