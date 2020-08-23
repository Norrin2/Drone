using Algorithm.Logic.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Logic.Tests
{
    [TestClass]
    public class DirectionUnitTest
    {
        [TestMethod]
        public void Input_Valido()
        {
            var movement = new Movement("N");
            Assert.IsNotNull(movement);
        }

        [TestMethod]
        public void Input_Vazio()
        {
            try
            {
                var movement = new Movement("");
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType());
                Assert.IsTrue(e.Message.Contains("não pode ser nulo ou vazio"));
            }
        }

        [TestMethod]
        public void Input_Direction_Invalida()
        {
            try
            {
                var movement = new Movement("ççç");
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentException), e.GetType());
                Assert.IsTrue(e.Message.Contains("direção inválida"));
            }
        }

        [TestMethod]
        public void Input_Steps_Invalidos()
        {
            try
            {
                var movement = new Movement("NAA9A89A89");
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentException), e.GetType());
                Assert.IsTrue(e.Message.Contains("nº de passos inválido"));
            }
        }
    }
}
