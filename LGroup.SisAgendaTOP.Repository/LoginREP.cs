using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//IMPORTAMOS E SUBIMOS PARA A MEMORIA
//A NAMESPACE DATAACCESS E MODEL (PROPRIEDADES)
using LGroup.SisAgendaTOP.DataAccess;
using LGroup.SisAgendaTOP.Model;

namespace LGroup.SisAgendaTOP.Repository
{
    public class LoginREP
    {
        public bool ValidarLogin(LoginMOD dadosTela_)
        {
            //ABRIMOS UMA CONEXAO COM O BANCO DE DADOS
            using (var conexao = new INCUBADORAEntities())
            {
                //ANY: FUNÇÃO DE VALIDACAO/PESQUISA
                //COM O RETORNO LOGICO
                //TRUE/FALSE
                return conexao.TB_LOGIN.Any(x=>x.NM_LOGIN == dadosTela_.Nome && x.DS_SENHA == dadosTela_.Senha);
            }
        }
    }
}
