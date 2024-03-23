using Desafio_Processo.UI.Models;

namespace Desafio_Processo.API.Models
{
    public class Produtos_em_Estoque
    {
        [key]
        public int Id { get; set; }
        public string? codpro { get; set; }
        public string? NomeProduto { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Pr_Venda { get; set; }
        public string? Descricao { get; set; }
    }
}
