using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Dtos;
using PizzaApi.Models;
using PizzaApi.Services;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ProdutoService produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }


        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var produtos = await produtoService.Listar();

                return new JsonResult(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] ProdutoDto produtoDto)
        {
            try
            {
                var produto = await produtoService.Gravar(new Produto
                {
                    Nome = produtoDto.Nome,
                    Valor = produtoDto.Valor
                });

                return new JsonResult(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
