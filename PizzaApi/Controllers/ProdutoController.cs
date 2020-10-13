using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Models;
using PizzaApi.Services;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController
    {
        private ProdutoService produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }


        [HttpGet]
        public async Task<IActionResult> GetByUsuario(int IdUsuario)
        {
            await produtoService.Gravar(new Produto("3 Queijos", 50));
            await produtoService.Gravar(new Produto("Frango com requeijão", 59.99));
            await produtoService.Gravar(new Produto("Mussarela", 42.50));
            await produtoService.Gravar(new Produto("Calabresa", 42.50));
            await produtoService.Gravar(new Produto("Pepperoni", 55));
            await produtoService.Gravar(new Produto("Veggie", 59.99));

            var pedidos = await produtoService.Listar();

            return new JsonResult(pedidos);
        }

    }
}
