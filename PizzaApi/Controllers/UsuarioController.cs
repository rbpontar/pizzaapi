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
    public class UsuarioController : ControllerBase
    {
        private UsuarioService usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = usuarioDto.Nome,
                    Endereco = new Endereco
                    {
                        Logradouro = usuarioDto.Endereco.Logradouro,
                        Numero = usuarioDto.Endereco.Numero,
                        Complemento = usuarioDto.Endereco.Complemento
                    }
                };

                usuario = await usuarioService.Gravar(usuario);

                return new JsonResult(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    e.Message
                });

            }
        }

        [HttpGet("{IdUsuario}")]
        public async Task<IActionResult> BuscarPorId(int IdUsuario)
        {
            try
            {
                var usuario = await usuarioService.BuscarPorId(IdUsuario);

                return new JsonResult(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    e.Message
                });
            }
        }

    }
}
