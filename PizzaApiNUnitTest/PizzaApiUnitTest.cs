using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using PizzaApi;
using PizzaApi.Dtos;
using PizzaApi.Models;

namespace PizzaApiNUnitTest
{
    public class Tests
    {

        private class ErrorMessage
        {
            public string message { get; set; }
        }

        private HttpClient TestHttpClient;

        [SetUp]
        public void Setup()
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            TestHttpClient = testServer.CreateClient();

            CadastrarPizza(new ProdutoDto { Nome = "3 Queijos", Valor = 50 });
            CadastrarPizza(new ProdutoDto { Nome = "Frango com Requeijão", Valor = 59.99 });
            CadastrarPizza(new ProdutoDto { Nome = "Mussarela", Valor = 42.50 });
            CadastrarPizza(new ProdutoDto { Nome = "Calabresa", Valor = 42.50 });
            CadastrarPizza(new ProdutoDto { Nome = "Peperoni", Valor = 55 });
            CadastrarPizza(new ProdutoDto { Nome = "Portuguesa", Valor = 45 });
            CadastrarPizza(new ProdutoDto { Nome = "Verggie", Valor = 59.99 });
        }

        [Test]
        public void PedidoSemItem()
        {
            List<ItemPedidoDto> itens = new List<ItemPedidoDto>();

            PedidoDto pedido = new PedidoDto { IdUsuario = 1 };

            var jsonContent = JsonConvert.SerializeObject(pedido);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/pedido/enviar",
                contentString).Result;
            var resp = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<ErrorMessage>(resp);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.AreEqual("Pedido deve ter no mínimo 1 e no máximo 10 pizzas.",
                responseData.message);
        }

        [Test]
        public void PedidoMais10Itens()
        {
            List<ItemPedidoDto> itens = new List<ItemPedidoDto>();

            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });
            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });

            PedidoDto pedido = new PedidoDto { IdUsuario = 1 };

            var jsonContent = JsonConvert.SerializeObject(pedido);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/pedido/enviar",
                contentString).Result;
            var resp = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<ErrorMessage>(resp);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.AreEqual("Pedido deve ter no mínimo 1 e no máximo 10 pizzas.",
                responseData.message);

        }

        [Test]
        public void PedidoSemDadosEntrega()
        {
            List<ItemPedidoDto> itens = new List<ItemPedidoDto>();

            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });

            PedidoDto pedido = new PedidoDto { Itens = itens };

            var jsonContent = JsonConvert.SerializeObject(pedido);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/pedido/enviar",
                contentString).Result;
            var resp = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<ErrorMessage>(resp);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.AreEqual("Informar os dados da entrega.",
                responseData.message);

        }

        [Test]
        public void PedidoSemUsuarioComDadosEntrega()
        {
            List<ItemPedidoDto> itens = new List<ItemPedidoDto>();

            itens.Add(new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 });

            var enderecoDto = new EnderecoDto
            {
                Logradouro = "Rua da Silva",
                Numero = "209",
                Complemento = "Casa 10"
            };

            PedidoDto pedido = new PedidoDto
            {
                Itens = itens,
                enderecoDto = enderecoDto
            };

            var jsonContent = JsonConvert.SerializeObject(pedido);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/pedido/enviar",
                contentString).Result;
            var resp = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void PedidoComUsuarioSemDadosDeEntrega()
        {
            int IdUsuario = CadastrarUsuario();

            List<ItemPedidoDto> itens = new List<ItemPedidoDto>
            {
                new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 }
            };

            PedidoDto pedido = new PedidoDto
            {
                IdUsuario = IdUsuario,
                Itens = itens
            };

            var jsonContent = JsonConvert.SerializeObject(pedido);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/pedido/enviar",
                contentString).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void ListarPedidosDoUsuario()
        {
            int IdUsuario = CadastrarUsuario();

            List<ItemPedidoDto> itens = new List<ItemPedidoDto>
            {
                new ItemPedidoDto { IdProduto1 = 1, IdProduto2 = 2 }
            };

            PedidoDto pedidoDto = new PedidoDto
            {
                IdUsuario = IdUsuario,
                Itens = itens
            };

            var jsonContent = JsonConvert.SerializeObject(pedidoDto);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/pedido/enviar",
                contentString).Result;
            var resp = response.Content.ReadAsStringAsync().Result;

            var pedido = JsonConvert.DeserializeObject<Pedido>(resp);

            response = TestHttpClient.GetAsync("api/pedido/usuario/"
               + IdUsuario.ToString()).Result;
            resp = response.Content.ReadAsStringAsync().Result;
            var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(resp);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Assert.AreNotEqual(0, pedidos.Count);
        }

        public void CadastrarPizza(ProdutoDto produtoDto)
        {

            var jsonContent = JsonConvert.SerializeObject(produtoDto);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/produto",
                contentString).Result;


            produtoDto = new ProdutoDto { Nome = "Portuguesa", Valor = 45 };

            jsonContent = JsonConvert.SerializeObject(produtoDto);
            contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            response = TestHttpClient.PostAsync("api/produto",
                contentString).Result;

        }

        public int CadastrarUsuario()
        {
            var usuario = new UsuarioDto
            {
                Nome = "Rodolfo",
                Endereco = new EnderecoDto
                {
                    Logradouro = "Rua Marquês",
                    Numero = "200",
                    Complemento = "apto 201"
                }
            };

            var jsonContent = JsonConvert.SerializeObject(usuario);
            var contentString = new StringContent(jsonContent, Encoding.UTF8,
                "application/json");

            var response = TestHttpClient.PostAsync("api/usuario",
                contentString).Result;

            var resp = response.Content.ReadAsStringAsync().Result;
            var usuarioRespose = JsonConvert.DeserializeObject<Usuario>(resp);

            return usuarioRespose.Id;

        }
    }
}