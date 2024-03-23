using Microsoft.EntityFrameworkCore;

namespace Desafio_Processo.API.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\mthia\\source\\repos\\Projeto-API-Vendas\\Desafio_Processo.db");
        }

        public DbSet<Cadastros> Cadastros { get; set;}
        public DbSet<Produtos_em_Estoque> Produtos_em_Estoque { get; set;}
    }
}
