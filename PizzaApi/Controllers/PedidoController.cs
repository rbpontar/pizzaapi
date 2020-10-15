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

        public PedidoController(PedidoService pedidoService)
        {
            this.pedidoService = pedidoService;
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> BuscarPorUsuario(int id)
        {
            try
            {
                var pedidos = await pedidoService.BuscarPedidosUsuario(id);

                return new JsonResult(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var pedido = await pedidoService.BuscarPorId(id);

                return new JsonResult(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Enviar([FromBody] PedidoDto pedidoDto)
        {
            try
            {
                var pedido = await pedidoService.Gravar(pedidoDto);

                return new JsonResult(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
