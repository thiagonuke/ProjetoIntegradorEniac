using Desafio_Processo.API.Migrations;
using Desafio_Processo.API.Models;
using Desafio_Processo.UI.Models;

namespace Desafio_Processo.API.Interface
{
    public interface ILojaRepository
    {
        BaseRetorno PesquisarUsuario(string email);
        Cadastros RetornaUsuario(Cadastros dados);
        string CadastrarUsuario(Cadastros dados);
        List<Produtos_em_Estoque> PesquisaProdutos(string codpro);
        BaseRetorno CadastrarProduto(Produtos_em_Estoque dados);
        BaseRetorno ExcluirProduto(string codpro);
        BaseRetorno UpdateProd(Produtos_em_Estoque codpro);
        BaseRetorno BaixaProdutos(List<Produtos_em_Estoque> dados);


    }
}
