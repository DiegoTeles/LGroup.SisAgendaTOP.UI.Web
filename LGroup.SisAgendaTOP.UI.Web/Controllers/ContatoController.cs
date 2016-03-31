using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//SEMPRE QUE INSERIMOS UMA NAMESPACE IREMOS SUBIR OS DADOS 
//PARA MEMORIA
//IMPORTAMOS A DLL / NASMESPACE DE MODELOS (PROPRIEDADES)
using LGroup.SisAgendaTOP.Model;
//IMPORTAMOS A DDL / NAMESPACE DE REPOSITORY
using LGroup.SisAgendaTOP.Repository;

//DESCEMOS PARA MEMORIA A NAMESPACE SYSTEM.WEB.SECURITY
using System.Web.Security;


namespace LGroup.SisAgendaTOP.UI.Web.Controllers
{
    public class ContatoController : Controller
    {
        //CRIAMOS UMA VARIAVEL RESPONSAVEL POR ARMAZENAS
        //E DEIXAR VISIVEL OS METODOS DA DLL REPOSITORY
        private ContatoREP _banco = new ContatoREP();

        //CRIAMOS UM METODO PARA LISTAR OS SEXOS
        private void CarregarSexos()
        {
            //CRIAMOS UMA VARIAVEL APONTANDO PARA
            //O REPOSITORIO DE SEXOS
            var sexoRepositorio = new SexoREP();

            //VIEWBAG = SEPARA UM TRECHO DE MEMORIA
            //E ARMAZENA OBJETOS/DADOS
            //APÓS A SUA UTILIZAÇÃO É ESVAZIDO
            ViewBag.Sexo = new SelectList(sexoRepositorio.ListarSexo(),
                                          "CodigoSexo",
                                          "DescricaoSexo");
        }

        //SEMPRE QUE UM ACTIONRESULT ESTIVER SEM 
        //PARAMETROS EM CIMA
        //É UM GET (RENDERIZADOR)
        //ACTIONRESULT: VIEW/PAGE
        //AUTHORIZE: INFORMAMOS E DEFINIMOS QUE PARA ACESSAR
        //AS ACTIONS DEVEMOS NOS AUTENTICAR
        [Authorize]
        public ActionResult Cadastrar()
        {
            //SEMPRE ANTES DE RENDERIZAR A TELA
            //E ELA TIVER LISTAS
            //DEVEMOS POPULAR
            CarregarSexos();

            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ContatoMOD dadosTela_)
        {
            try //SUCESSO
            {
                //COLETAMOS OS OBJETOS DA DELA E 
                //ENVIAMOS PARA O METODO CADASTRAR (REPOSITORY)
                _banco.Cadastrar(dadosTela_);

                //DISPARAMOS UMA MENSAGEM DE SUCESSO
                //TEMPDATA: SEPARAMOS UM TRECHO E INSERIMOS UM
                //IDENTIFICADOR POR PARAMETRO
                //NESTE CASO POSSO INICIALIZAR A PARTIR DE UM SCRIPT
                TempData.Add("Mensagem", "Usuario cadastro com Sucesso");

                //APOS INSERIR UM NOVO CADASTRO
                //MANDAMOS REDIRECIONAR PARA A TELA LISTAR
                return RedirectToAction("Listar");
            }
            catch (Exception)//ERRO
            {

                //SEMPRE QUE A TELA FOR RENDERIZADA
                //DEVEMOS CHAMAR O METODO DE LISTAR OS SEXOS
                CarregarSexos();

                //DISPARAMOS A MENSAGEM DE ERRO
                TempData.Add("Mensagem", "Ops! Erro ao cadastrar");

                return View();
            }

        }

        [Authorize]
        public ActionResult Listar()
        {
            //CHAMAMOS A VARIAVEL QUE ESTA
            //APONTADO PARA O REPOSITORIO
            var _contato = _banco.ListarContato();

            return View(_contato);
        }

        [Authorize]
        public ActionResult Editar(int id)
        {
            //ANTES DE RENDERIZAR A VIEW E ELA TIVER
            //LISTAS DEVEMOS PREENCHER
            CarregarSexos();

            //CHAMAMOS A VARIAVEL QUE ESTA APONTANDO
            //PARA O REPOSITORIO
            var contato = _banco.PesquisarPorCodigo(id);

            return View(contato);
        }

        [HttpPost]
        public ActionResult Editar(ContatoMOD dadosTela_, int id)
        {
            try//SUCESSO
            {
                dadosTela_.CodigoContato = id;
                //CHAMAMOS O METODO DE EDITAR
                _banco.Atualizar(dadosTela_);

                //DISPARAMOS A MENSAGEM DE SUCESSO
                TempData.Add("Mensagem", "Contato atualizado com Sucesso");

                //MANDAMOS REDIRECIONAR PARA A 
                //ACTION LISTA
                return RedirectToAction("Listar");
            }
            catch (Exception)//ERRO
            {
                //CARREGAMOS A LISTA DE SEXOS
                CarregarSexos();

                //DISPARAMOS A MENSAGEM DE ERRO
                TempData.Add("Mensagem", "Ops! Erro ao Atualizar");

                return View();
            }

        }

        public ActionResult Deletar(int id)
        {
            try
            {
                //CHAMAMOS O METODE DE DELETAR 
                _banco.Deletar(id);

                //DISPARAMOS A MENSAGEM DE SUCESSO
                TempData.Add("Mensagem", "Contato deletado com Sucesso");

                //REDIRECIONAMOS PARA A ACTION LISTAR
                return RedirectToAction("Listar");
            }
            catch (Exception)
            {
                //DISPARAMOS A MENSAGEM DE ERRO
                TempData.Add("Mensagem", "Ops! Erro ao Deletar");

                //MANDAMOS REDIRECIONAR PARA A ACTION LISTAR
                return RedirectToAction("Listar");
            }
        }

        public ActionResult Login()
        {
            //SEMPRE QUE O USUARIO ACESSAR/RENDERIZAR
            //A TELA DE LOGIN
            //LIMPAMOS OS CACHES
            FormsAuthentication.SignOut();

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginMOD dadosTela_)
        {
            //CRIAMOS UMA VARIAVEL APONTANDO PARA O REPOSITORIO
            var loginRepositorio = new LoginREP();

            if (!loginRepositorio.ValidarLogin(dadosTela_))
            {

                //DISPARAMOS UMA MENSAGEM INFORMANDO QUE 
                //O USUARIO OU SENHA NÃO EXISTE
                TempData.Add("Mensagem", "Usuario ou Senha não existe");

                return View();
            }

            FormsAuthentication.SetAuthCookie(dadosTela_.Nome, true);

            return RedirectToAction("Cadastrar");
        }

        public ActionResult Logout()
        {
            //LIMPAMOS OS CACHES
            FormsAuthentication.SignOut();

            //REDIRECIONAMOS PARA A ACTION DE LOGIN
            return RedirectToAction("Login");
        }
    }
}