using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//IMPORTAMOS A DLL/NAMESPACE DATAACESS (DESCI PARA MEMORIA)
using LGroup.SisAgendaTOP.DataAccess;
//IMPORTAMOS A DDL/NAMESPACE DE MODELOS (PROPRIEDADES)
using LGroup.SisAgendaTOP.Model;

namespace LGroup.SisAgendaTOP.Repository
{
    public class ContatoREP
    {

        //IREMOS INSERIR O CRUD
        //CADASTRAR - OK
        //LISTAR - OK
        //ATUALIZAR - OK
        //DELETAR (ATUALIZAR) - OK
        //CRIAMOS O METODO DE CADASTRAR
        public void Cadastrar(ContatoMOD dadosTela_)
        {
            using (var conexao = new INCUBADORAEntities() )
            {//ABRIMOS A CONCEXÃO COM O DATAACCES

                //1° PASSO
                //CRIAMOS UMA VARIAL PARA OS OBJETOS DA TELA
                var _novoContato = new TB_CONTATO();

                _novoContato.NM_CONTATO = dadosTela_.Nome;
                _novoContato.DS_EMAIL = dadosTela_.Email;
                _novoContato.NR_TELEFONE = dadosTela_.Telefone;
                _novoContato.DT_NASCIMENTO = dadosTela_.DataNascimento;
                //_novoContato.ID_SEXO = 2;
                _novoContato.ID_SEXO = dadosTela_.Sexo.CodigoSexo;
                _novoContato.NR_CEP = dadosTela_.CEP;
                _novoContato.NM_LOGRADOURO = dadosTela_.Logradouro;
                _novoContato.NM_BAIRRO = dadosTela_.Bairro;
                _novoContato.NR_NUMERO = dadosTela_.Numero;
                _novoContato.DT_CADASTRO = DateTime.Now;
                _novoContato.CD_ATIVO = true;

                //2° PASSO
                //MANDAMOS ADICIONAR NA TABELA TB_CONTATO
                conexao.TB_CONTATO.Add(_novoContato);

                //3° PASSO
                //MANDAMOS SALVAR
                conexao.SaveChanges();

            }//FECHAMOS A CONEXÃO COM O DATAACCES
        }
        
        //CRIAMOS O METODO DE LISTAR
        //IREMOS UTILIZA O TO LINQ SQL (SERNIORZÃO)
        public List<ContatoMOD> ListarContato()
        {
            //ABRIMOS A CONEXAO COM O BANCO DE DADOS
            using (var conexao = new INCUBADORAEntities())
            {
                //IREMOS LISTAR UTILIZANDO O LINQ (MALANDROX)
                var retorno = (from C in conexao.TB_CONTATO
                               where C.CD_ATIVO == true
                               select new ContatoMOD 
                               {
                                   CodigoContato = C.ID_CONTATO,
                                   Nome = C.NM_CONTATO,
                                   Email = C.DS_EMAIL,
                                   Telefone = C.NR_TELEFONE,
                                   DataNascimento = C.DT_NASCIMENTO,
                                   Sexo = new SexoMOD
                                   {
                                       CodigoSexo = C.ID_SEXO,
                                       DescricaoSexo = C.TB_SEXO.DS_SEXO
                                   },
                                   CEP = C.NR_CEP ?? "PENDENTE",
                                   Logradouro = C.NM_LOGRADOURO ?? "PENDENTE",
                                   Bairro = C.NM_BAIRRO ?? "PENDENTE",
                                   Numero = C.NR_NUMERO ?? "PENDENTE"
                               }).ToList();
                return retorno;
            }
        }

        //CRIAMOS O METODO DE ATUALIZAÇÃO
        public void Atualizar(ContatoMOD dadosTela_)
        {
            //ABRIMOS A CONEXAO COM O BANCO DE DADOS
            using (var conexao = new INCUBADORAEntities())
            {
                //1° VERIFICAMOS SE O USUARIO
                var editarContato = conexao.TB_CONTATO.Single(x=>x.ID_CONTATO == dadosTela_.CodigoContato);

                //2° INSERIMOS OS NOVOS DADOS
                editarContato.NM_CONTATO = dadosTela_.Nome;
                editarContato.DS_EMAIL = dadosTela_.Email;
                editarContato.NR_TELEFONE = dadosTela_.Telefone;
                editarContato.DT_NASCIMENTO = dadosTela_.DataNascimento;
                editarContato.ID_SEXO = dadosTela_.Sexo.CodigoSexo;
                editarContato.NR_CEP = dadosTela_.CEP;
                editarContato.NM_LOGRADOURO = dadosTela_.Logradouro;
                editarContato.NM_BAIRRO = dadosTela_.Bairro;
                editarContato.NR_NUMERO = dadosTela_.Numero;
                editarContato.DT_ALTERACAO = DateTime.Now;

                //3° MANDAMOS SALVAR NA TABELA (UPDATE)
                conexao.SaveChanges();
            }
        }

        //CRIAMOS O METODO DE DELETAR
        public void Deletar(int codigo_)
        {
            //ABRIMOS A CONEXAO COM O BANCO DE DADOS
            using (var conexao = new INCUBADORAEntities())
            {
                //1° - VERIFICAMOS SE O USUARIO EXISTE
                var deletarContato = conexao.TB_CONTATO.Single(x=>x.ID_CONTATO == codigo_);

                //2° - ALTERAMOS OS CAMPOS DA TABELA
                //ALTERAMOS (DT_ALTERACAO (DATA ATUAL) - CD_ATIVO (FALSE))
                deletarContato.DT_ALTERACAO = DateTime.Now;
                deletarContato.CD_ATIVO = false;

                //3° - MANDAMOS SALVAR NO BANCO
                conexao.SaveChanges();
            }
        }

        //CRIAMOS UM METODO PARA PESQUISAR POR CÓDIGO
        //CASO O USUARIO EXISTE IREMOS COLOCAR OS DADOS/OBJETOS
        //EM MODO DE EDIÇÃO
        public ContatoMOD PesquisarPorCodigo(int codigo)
        {
            //ABRIMOS A CONEXAO COM O BD
            using (var conexao = new INCUBADORAEntities())
            {
                var _contatoTabela = conexao.TB_CONTATO.Single(x => x.ID_CONTATO == codigo);

                return new ContatoMOD
                {
                    CodigoContato = _contatoTabela.ID_CONTATO,
                    Nome = _contatoTabela.NM_CONTATO,
                    Email = _contatoTabela.DS_EMAIL,
                    Telefone = _contatoTabela.NR_TELEFONE,
                    DataNascimento = _contatoTabela.DT_NASCIMENTO,
                    Sexo = new SexoMOD
                    {
                        CodigoSexo = _contatoTabela.ID_SEXO,
                        DescricaoSexo = _contatoTabela.TB_SEXO.DS_SEXO
                    }
                };
            }
        }
    }
}
