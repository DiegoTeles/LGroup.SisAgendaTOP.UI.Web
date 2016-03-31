using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// DESCEMOS PARA A MEMORIA O PROJETO DE REPOSITORIO
using LGroup.SisAgendaTOP.Repository;

namespace LGroup.SisAgendaTOP.Test
{
    [TestClass]
    public class ContatoTest
    {
        private SexoREP _repositorio = new SexoREP();
        [TestMethod]
        public void TesteVerificarSexoIgualNull()
        {
            var registro = _repositorio.ListarSexo();
            Assert.IsNull(registro);
        }
        [TestMethod]
        public void TesteVerificarSexoIgual2()
        {
            var registro = _repositorio.ListarSexo();

            Assert.AreEqual(2, registro.Count);

        }
    }
}
