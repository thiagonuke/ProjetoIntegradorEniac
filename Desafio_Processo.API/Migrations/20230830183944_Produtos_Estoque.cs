using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Processo.API.Migrations
{
    /// <inheritdoc />
    public partial class Produtos_Estoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Produtos_em_Estoque",
               columns: table => new
               {
                   Id = table.Column<int>(type: "INTEGER", nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                   codpro = table.Column<string>(type: "TEXT", nullable: true),
                   NomeProduto = table.Column<string>(type: "TEXT", nullable: true),
                   Quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                   Pr_Venda = table.Column<decimal>(type: "TEXT", nullable: true),
                   Descricao = table.Column<string>(type: "TEXT", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Produtos", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Produtos");
        }
    }
}
