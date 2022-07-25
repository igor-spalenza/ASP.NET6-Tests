using ASP.NET6_API.Controllers;
using ASP.NET6_API.Models;
using ASP.NET6_API.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiTests
{
    [TestClass]
    public class ClienteControllerTest
    {

        public List<Cliente> Clientes = new List<Cliente>();

        [TestInitialize]
        public void start()
        {
            Clientes.Add(new Cliente { Nome = "Igor Spalenza", Email = "igor@gmail.com" });
        }

        [TestMethod]
        public void Pegar_Todos_Clientes()
        {
            Mock<IClienteService> mock = new Mock<IClienteService>();
            mock.Setup(x => x.RecuperarClientes()).Returns(Clientes);

            ClienteController controller = new ClienteController(mock.Object);
            var retorno = controller.RecuperarClientes();

            Assert.AreEqual(Clientes, retorno);
        }

        [TestMethod]
        public void Inserir_Cliente()
        {
            Mock<IClienteService> mock = new Mock<IClienteService>();
            mock.Setup(x => x.AdicionarCliente(Clientes.FirstOrDefault())).Returns(Clientes.FirstOrDefault());

            ClienteController controller = new ClienteController(mock.Object);
            IActionResult inserir = controller.AdicionarCliente(Clientes.FirstOrDefault());
            var statusCode = inserir.GetType().GetProperty("StatusCode").GetValue(inserir);
            var objeto = inserir.GetType().GetProperty("Value").GetValue(inserir);

            Assert.AreEqual(200, statusCode);
            Assert.AreEqual(Clientes.FirstOrDefault(), objeto);
        }
    }
}
