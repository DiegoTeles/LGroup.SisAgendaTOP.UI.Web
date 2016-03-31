using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//IMPORTAMOS A NAMESPACE DE MODELOS (PROPRIEDADES)
using LGroup.SisAgendaTOP.Model;

//IMPORTAMOS A NAMESPACE DE ACESSO A DADOS (EDMX)
using LGroup.SisAgendaTOP.DataAccess;

namespace LGroup.SisAgendaTOP.Repository
{
    public class SexoREP
    {
        //CRIAMOS O METODO PARA LISTA OS SEXOS
        //IREMOS UTILIZAR O TO LINQ SQL
        public List<SexoMOD> ListarSexo()
        {
            //ABRIMOS A CONEXAO COM BANCO
            using (var conexao = new INCUBADORAEntities())
            {
                //IREMOS UTILIZAR O LINQ
                var retornoSexo = (from S in conexao.TB_SEXO
                                   select new SexoMOD 
                                   {
                                       CodigoSexo = S.ID_SEXO,
                                       DescricaoSexo = S.DS_SEXO
                                   }).ToList();
                return retornoSexo;
            }
        }
    }
}
