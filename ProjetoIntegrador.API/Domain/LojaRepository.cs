using Desafio_Processo.API.Interface;
using Desafio_Processo.UI.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Desafio_Processo.API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Desafio_Processo.API.Migrations;

namespace Desafio_Processo.API.Domain

{
    public class LojaRepository : ILojaRepository
    {

        private readonly IDbConnection _dbConnection;
        private readonly AppDbContext _appDbContext;

        public LojaRepository(IDbConnection dbConnection, AppDbContext appDb) { 
        
            _dbConnection = dbConnection;
            _appDbContext = appDb;
        }

        #region Metodos Usuario/Login
        public BaseRetorno PesquisarUsuario(string email)
        {
            try
            {
                var cadastro = _appDbContext.Cadastros.Where(t => t.Email == email).ToList();
                if(cadastro.Count > 0) { return new BaseRetorno() {Mensagem = "Existente", Status = true}; } else { return new BaseRetorno() {Mensagem = "Não Existente", Status = false}; }

            } catch (Exception ex) { return new BaseRetorno() { Mensagem = "Erro", Status = false }; }
          
        }

        public string CadastrarUsuario(Cadastros dados)
        {
            try
            {
                dados.Grupo = "Cliente";
                _appDbContext.Cadastros.Add(dados);
                _appDbContext.SaveChanges();

                return "Success";

            }
            catch (Exception ex) { return "Error"; }
        }

        public Cadastros RetornaUsuario(Cadastros dados)
        {
            var cadastro = _appDbContext.Cadastros.Where(t => t.Email == dados.Email && t.senha == dados.senha).FirstOrDefault();
            return cadastro;
        }
        #endregion

        #region Metodos Estoque
        public List<Produtos_em_Estoque> PesquisaProdutos(string codpro)
        {
            var response = new List<Produtos_em_Estoque>(); 

            try 
            {
                if(codpro == null)
                {
                    response = _appDbContext.Produtos_em_Estoque.ToList();
                }
                else
                {
                    response = _appDbContext.Produtos_em_Estoque.Where(t => t.codpro == codpro).ToList();
                }


            }catch (Exception ex) { }

            return response;
        }

        public BaseRetorno CadastrarProduto(Produtos_em_Estoque dados)
        {
            var response = new BaseRetorno();
            try
            {

                _appDbContext.Produtos_em_Estoque.Add(dados);
                _appDbContext.SaveChanges();
                response.Mensagem = "Sucesso: Produto Cadastrado";
                response.Status = true;

                return response;
            }
            catch(Exception ex) { return new BaseRetorno() {Mensagem = "Erro ao inserir", Status = false }; }
            
        }

        public BaseRetorno ExcluirProduto(string codpro)
        {
            try
            {
                _appDbContext.Produtos_em_Estoque.Where(t => t.codpro == codpro).ExecuteDelete();
                return new BaseRetorno() {Mensagem = "Excluido com sucesso", Status = true};
            }
            catch (Exception ex) { return new BaseRetorno() { Mensagem = "Erro ao excluir", Status = false }; }    
        }

        public BaseRetorno UpdateProd(Produtos_em_Estoque dados)
        {
            try
            {
                _appDbContext.Produtos_em_Estoque.Where(t => t.codpro == dados.codpro).ExecuteUpdate(t => 
                t.SetProperty(th => th.codpro, dados.codpro)
                .SetProperty(th => th.NomeProduto, dados.NomeProduto)
                .SetProperty(th => th.Quantidade, dados.Quantidade)
                .SetProperty(th => th.Descricao, dados.Descricao)
                .SetProperty(th => th.Pr_Venda, dados.Pr_Venda)
                
                );
                   
                return new BaseRetorno() { Mensagem = "Atualizado com sucesso", Status = true };
            }
            catch (Exception ex) { return new BaseRetorno() { Mensagem = "Erro ao atualizar", Status = false }; }
        }

        public BaseRetorno BaixaProdutos(List<Produtos_em_Estoque> dados)
        {
            foreach(var p in dados)
            {
                try
                {
                    var prod = _appDbContext.Produtos_em_Estoque.Where(t => t.codpro == p.codpro).ToList().FirstOrDefault();
                    var baixa = prod.Quantidade - 1;
                    if (baixa < 0)
                    {
                        baixa = 0;
                    }

                    _appDbContext.Produtos_em_Estoque.Where(t => t.codpro == p.codpro).ExecuteUpdate(t =>
                        t.SetProperty(th => th.Quantidade, baixa)

                        );
                    
                }
                catch (Exception ex) { }
            }

            return new BaseRetorno() { Mensagem = "Finalizado com sucesso", Status = true };
        }


        #endregion
    }
}
