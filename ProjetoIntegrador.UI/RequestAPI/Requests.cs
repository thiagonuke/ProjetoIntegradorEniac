using Desafio_Processo.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Desafio_Processo.UI.RequestAPI
{
    public class Requests
    {

        HttpClient _client = new HttpClient();
        
        public Requests() {

            SetarUri();
        }

        #region Request Usuarios
        public async Task<CadastrosViewModel> RetornaUsuario(CadastrosViewModel dados)
        {
           
            return await _client.PostAsJsonAsync<CadastrosViewModel>("Loja/RetornaUsuario", dados).Result.Content.ReadFromJsonAsync<CadastrosViewModel>();
        }

        public async Task<BaseRetorno> PesquisaUsuario(string email)
        {
            
            return await _client.GetFromJsonAsync<BaseRetorno>($"Loja/PesquisarUsuario?email={email}");

        } 
        public async Task<string> CadastrarUsuario(CadastrosViewModel dados)
        {
            return await _client.PostAsJsonAsync<CadastrosViewModel>($"Loja/CadastrarUsuario", dados).Result.Content.ReadFromJsonAsync<string>();

        }
        #endregion

        #region Request Produtos
        public async Task<List<Produtos_em_EstoqueViewModel>> PesquisaProdutos(string codpro)
        {
            try
            {
                var tst = await _client.GetFromJsonAsync<List<Produtos_em_EstoqueViewModel>>($"Loja/PesquisaProdutos?codpro={codpro}");
                return tst;

            }catch (Exception ex) { return null; }
        }
        public async Task<BaseRetorno> CadastrarProduto(Produtos_em_EstoqueViewModel dados)
        {
            return await _client.PostAsJsonAsync<Produtos_em_EstoqueViewModel>("Loja/CadastrarProduto", dados).Result.Content.ReadFromJsonAsync<BaseRetorno>();
        }

        public async Task<BaseRetorno> ExcluirProd(string codpro)
        {
            var delete = await _client.DeleteAsync($"Loja/ExcluirProd?codpro={codpro}");

            if (delete.IsSuccessStatusCode)
            {
                return new BaseRetorno() { Mensagem = "Excluido", Status = true };
            }
            else { return new BaseRetorno() { Mensagem = "Erro", Status = false }; }
            
        }
        public async Task<BaseRetorno> AtualizarProd(Produtos_em_EstoqueViewModel dados)
        {
            try
            {
                return await _client.PutAsJsonAsync<Produtos_em_EstoqueViewModel>($"Loja/AtualizaProd", dados).Result.Content.ReadFromJsonAsync<BaseRetorno>();

            }
            catch (Exception ex) { return new BaseRetorno() { Mensagem = "Erro", Status = false }; }

        }

        public async Task<BaseRetorno> BaixaItens(List<Produtos_em_EstoqueViewModel> dados)
        {
            return await _client.PostAsJsonAsync<List<Produtos_em_EstoqueViewModel>>("Loja/BaixaProdutos", dados).Result.Content.ReadFromJsonAsync<BaseRetorno>();
        }

        #endregion

        private void SetarUri()
        {
            _client.BaseAddress = new Uri("https://localhost:7223/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
