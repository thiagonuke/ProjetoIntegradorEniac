using Desafio_Processo.UI.Models;
using Desafio_Processo.UI.RequestAPI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Desafio_Processo.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Requests _request;
        private string SessaoUsuario = string.Empty;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, Requests request)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _request = request;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (VerifiLogin())
                {
                    var usuario = JsonConvert.DeserializeObject<CadastrosViewModel>(SessaoUsuario);

                    var pesquisaProdutosPListar = await _request.PesquisaProdutos(null);

                    var dadosView = new Produtos_em_EstoqueViewModel();

                    foreach(var i in pesquisaProdutosPListar)
                    {
                        if(i.Quantidade > 0 && i.Grupo == "Cliente") {
                            dadosView.prods.Add(i);
                        }
                        else
                        {
                            dadosView.prods.Add(i);
                        }
                    }


                    dadosView.Nome = usuario.Nome == null ? "" : usuario.Nome;
                    dadosView.Grupo = usuario.Grupo;


                    return View("../Home/Index", dadosView);
                }
                else
                {
                    return View("../Login/Index");
                }

            }
            catch(Exception ex) { return View("../Login/Index"); }
            
        }

        public bool VerifiLogin()
        {
            bool response = false;
            try
            {
                SessaoUsuario = HttpContext.Session.GetString("USUARIOLOGADO");

                response = SessaoUsuario == null ? false : true;

            }
            catch (Exception ex) { return false;  }
            
            return response;
        }

        public ActionResult Login(CadastrosViewModel dados)
        {
            try
            {
                //consultabanco
                var usuario = _request.RetornaUsuario(dados).Result;

                if(usuario.senha != null)
                {
                    var SessaoSerializada = JsonConvert.SerializeObject(usuario);
                    HttpContext.Session.SetString("USUARIOLOGADO", SessaoSerializada);

                    return Json("Encontrado");
                }
                else
                {
                    return Json("Erro");
                }


            }catch(Exception ex) { }

            return View();
        }

        public IActionResult CadastroUsuario(CadastrosViewModel dados)
        {
            try
            {
                //pesquisa se existe
                var usuario = _request.PesquisaUsuario(dados.Email).Result;

                if (usuario.Mensagem != "Existente")
                {
                    //cadastra
                    string insert = _request.CadastrarUsuario(dados).Result;

                    return Json(insert);
                }
                else
                {
                    return Json(usuario.Mensagem);

                }

            }
            catch (Exception ex) { return Json("Erro"); }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USUARIOLOGADO");
            return View("../Login/Index");
        }

    }
}