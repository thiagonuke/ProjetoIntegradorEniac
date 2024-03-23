namespace Desafio_Processo.UI.Models
{
    public class Produtos_em_EstoqueViewModel
    {
        public int Id { get; set; }
        public string? codpro { get; set; }
        public string? NomeProduto { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Pr_Venda { get; set; }
        public string? Descricao { get; set; }
        public string? Nome { get; set; }
        public string? Grupo { get; set; }

        public List<Produtos_em_EstoqueViewModel>? prods { get; set; } = new List<Produtos_em_EstoqueViewModel>();

    }
}
