using Desafio_Processo.API.Interface;
using Desafio_Processo.API.Migrations;
using Desafio_Processo.API.Models;
using Desafio_Processo.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Processo.API.Controllers
{
    [Route("[controller]")]
    public class LojaController : Controller
    {
        private ILojaRepository _repo;

        public LojaController(ILojaRepository repo)
        {
            _repo = repo;
        }
        //metodo teste
        [HttpGet("testeapi")]
        public ActionResult testeapi()
        {
            string tst = "deu certo";

            return Json(tst);
        }

        #region Metodos usuario/login
        [HttpPost("RetornaUsuario")]
        public Cadastros RetornaUsuario([FromBody]Cadastros dados)
        {
            try
            {
                Cadastros retorna = _repo.RetornaUsuario(dados);
                if(retorna != null) { return retorna; } else { return new Cadastros(); }
            }
            catch(Exception ex) { return null; }
        }

        [HttpGet("PesquisarUsuario")]
        public BaseRetorno PesquisarUsuario(string Email)
        {
            try
            {
                BaseRetorno response = _repo.PesquisarUsuario(Email);

                return response;
                

            }catch (Exception ex)
            {
                return new BaseRetorno { Mensagem = "Erro", Status = false };
            }

        }

        [HttpPost("CadastrarUsuario")]
        public string CadastrarUsuario([FromBody]Cadastros dados)
        {
            try
            {
                string response = _repo.CadastrarUsuario(dados);
                return response;
            }
            catch (Exception ex) { return "Error"; }
        }
        #endregion

        #region Metodos Produtos
        [HttpGet("PesquisaProdutos")]
        public List<Produtos_em_Estoque> PesquisaProdutos(string codpro)
        {
            try
            {
                var response = _repo.PesquisaProdutos(codpro);

                return response;

            }catch (Exception ex) { return null; }

        }

        [HttpPost("CadastrarProduto")]
        public BaseRetorno CadastrarProduto([FromBody]Produtos_em_Estoque dados)
        {
            var rsp = new BaseRetorno();

            try
            {
                rsp = _repo.CadastrarProduto(dados);

            }catch(Exception ex) { rsp.Mensagem = "Erro"; rsp.Status = false; }
            return rsp;
        }

        [HttpDelete("ExcluirProd")]
        public IActionResult ExcluirProd(string codpro)
        {
            try
            {
                var retorno = _repo.ExcluirProduto(codpro);

                if(retorno.Status == true)
                {
                    return Ok("Excluido");
                }
                else { return BadRequest("Erro"); }
            }
            catch (Exception ex) { return BadRequest("Erro"); }   
            
        }

        [HttpPut("AtualizaProd")]
        public BaseRetorno AtualizaProd([FromBody]Produtos_em_Estoque dados)
        {
            var response = new BaseRetorno();
            try
            {
                var retorno = _repo.UpdateProd(dados);

                if (retorno.Status == true)
                {
                    response.Mensagem = "Atualizado com sucesso!";
                    response.Status = true;
                }
                else {
                    response.Mensagem = "Erro ao atualizar!";
                    response.Status = false;
                }
            }
            catch (Exception ex) {
                response.Mensagem = "Erro ao atualizar!";
                response.Status = false;
            }
            return response;
        }
        [HttpPost("BaixaProdutos")]
        public BaseRetorno BaixaProdutos([FromBody]List<Produtos_em_Estoque> dados)
        {
            try
            {
                var retorno = _repo.BaixaProdutos(dados);

                return new BaseRetorno() { Mensagem = "Finaliado com sucesso", Status = true };
            }
            catch (Exception ex) { return new BaseRetorno() { Mensagem = "Erro ao finalizar compra", Status = false }; }

        }

        #endregion

    }
}
