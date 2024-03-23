using Desafio_Processo.UI.Models;
using Desafio_Processo.UI.RequestAPI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Desafio_Processo.UI.Controllers
{
    public class ProdutosController : Controller
    {
        private Requests _request;

        public ProdutosController(Requests request)
        {
            _request = request;
        }

        #region Funções Produto/carrinho
        public IActionResult CadastrarProduto(Produtos_em_EstoqueViewModel dados) {
            try
            {
                var prodExistente = _request.PesquisaProdutos(dados.codpro).Result;

                if(prodExistente.Count > 0)
                {
                    return Json("Produto com código já cadastrado!");
                }
                else
                {
                    var insertProd = _request.CadastrarProduto(dados).Result;

                    if(insertProd.Status == true)
                    {
                        return Json("Inserido com sucesso!");
                    }
                    else { return Json("Erro ao inserir produto"); }

                }
            }catch(Exception ex) { return Json("Erro ao se comunicar com servidor"); }
        
        
        
        }

        public IActionResult ListarProdutoEdit(string codpro)
        {
            try
            {
                var listarprod = _request.PesquisaProdutos(codpro).Result.FirstOrDefault();

                return Json(listarprod);

            }catch(Exception ex) { return Json("Erro"); }
        }

        public IActionResult ExcluirProduto(string codpro)
        {
            try
            {
                var excluirProd = _request.ExcluirProd(codpro).Result;

                if (excluirProd.Status == true)
                {
                    return Json("Excluido com sucesso");
                }
                else { return Json("Erro ao excluir"); }
            } catch (Exception ex) { return Json("Erro"); }
        }

        public IActionResult SalvarEditProd(Produtos_em_EstoqueViewModel dados)
        {
            try
            {
                var update = _request.AtualizarProd(dados).Result;

                return Json(update);
            }catch(Exception ex) { return Json("Erro"); }
        }

        public void AdicionarCart(string codpro)
        {
            string Sessaoprodutos = string.Empty; 
            try
            {
                try
                {
                    Sessaoprodutos = HttpContext.Session.GetString("PRODUTOSCART");

                    var prod = _request.PesquisaProdutos(codpro).Result.FirstOrDefault();

                    var produtos = JsonConvert.DeserializeObject<List<Produtos_em_EstoqueViewModel>>(Sessaoprodutos);
                    produtos.Add(prod);

                    var SessaoProdutosSerializada = JsonConvert.SerializeObject(produtos);

                    HttpContext.Session.SetString("PRODUTOSCART", SessaoProdutosSerializada);
                }
                catch(Exception ex) {

                    var produtos = new List<Produtos_em_EstoqueViewModel>();

                    var prod = _request.PesquisaProdutos(codpro).Result.FirstOrDefault();

                    produtos.Add(prod);


                    var SessaoProdSerializada = JsonConvert.SerializeObject(produtos);

                    HttpContext.Session.SetString("PRODUTOSCART", SessaoProdSerializada);
                }

                

            }catch(Exception ex) { }    
        }

        public List<Produtos_em_EstoqueViewModel> ProdutosCarrinho()
        {
            try
            {
                var Sessaoprodutos = HttpContext.Session.GetString("PRODUTOSCART");
                var produtos = JsonConvert.DeserializeObject<List<Produtos_em_EstoqueViewModel>>(Sessaoprodutos);

                return produtos;
            }
            catch(Exception ex) { return null; }    
        }
        public void ExcluirProdutoCarrinho(string codpro)
        {
            try
            {
                string Sessaoprodutos = string.Empty;
                Sessaoprodutos = HttpContext.Session.GetString("PRODUTOSCART");

                var produtos = JsonConvert.DeserializeObject<List<Produtos_em_EstoqueViewModel>>(Sessaoprodutos);

                var prod = produtos.FirstOrDefault(t => t.codpro == codpro);

                produtos.Remove(prod);

                var SessaoProdSerializada = JsonConvert.SerializeObject(produtos);

                HttpContext.Session.SetString("PRODUTOSCART", SessaoProdSerializada);

            }
            catch(Exception ex) { }
        }
        #endregion

        #region finalizar compra
        public IActionResult FinalizarCompra()
        {
            try
            {
                var Sessaoprodutos = HttpContext.Session.GetString("PRODUTOSCART");
                var produtos = JsonConvert.DeserializeObject<List<Produtos_em_EstoqueViewModel>>(Sessaoprodutos);

                var baixaNosItemsEstoque = _request.BaixaItens(produtos).Result;
                if(baixaNosItemsEstoque.Status == true)
                {
                    HttpContext.Session.Remove("PRODUTOSCART");
                    return Json("Items comprados com sucesso!");
                }
                else
                {
                    return Json("Erro ao tentar se comunicar com servidor, tente novament mais tarde!");
                }

            }
            catch(Exception ex) { return Json("Erro"); }
        }
        #endregion
    }


}
