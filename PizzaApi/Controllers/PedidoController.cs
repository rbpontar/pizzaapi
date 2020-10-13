using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Dtos;
using PizzaApi.Models;
using PizzaApi.Repositories;
using PizzaApi.Services;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private PedidoService pedidoService;
        private ProdutoService produtoService;

        public PedidoController(PedidoService pedidoService, ProdutoService produtoService)
        {
            this.pedidoService = pedidoService;
            this.produtoService = produtoService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> BuscarPorUsuario(int IdUsuario)
        {

            //await produtoService.Gravar(new Produto("3 Queijos", 50));
            //await produtoService.Gravar(new Produto("Frango com requeijão", 59.99));
            //await produtoService.Gravar(new Produto("Mussarela", 42.50));
            //await produtoService.Gravar(new Produto("Calabresa", 42.50));
            //await produtoService.Gravar(new Produto("Pepperoni", 55));
            //await produtoService.Gravar(new Produto("Veggie", 59.99));

            //List<ItemPedido> itens = new List<ItemPedido>();

            //itens.Add(new ItemPedido()
            //{
            //    IdProduto1 = 1,
            //    IdProduto2 = 1

            //});

            //itens.Add(new ItemPedido()
            //{
            //    IdProduto1 = 2,
            //    IdProduto2 = 3
            //});

            //var pedido = new Pedido
            //{
            //    Nome = "Pedido 1",
            //    Itens = itens
            //};

            //Endereco endereco = new Endereco
            //{
            //    Logradouro = "Rua da Lapa ",
            //    Complemento = "apto 109",
            //    Numero = "300"
            //};

            //pedido.Endereco = endereco;

            //await pedidoService.Gravar(pedido);

            var pedidos = await pedidoService.ListarPedidosUsuario(IdUsuario);

            return new JsonResult(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var pedido = await pedidoService.GetById(id);

                return new JsonResult(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> Enviar([FromBody] PedidoDto pedido)
        {
            try
            {
                var result = await pedidoService.Gravar(pedido);

                return Ok(new
                {
                    result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
