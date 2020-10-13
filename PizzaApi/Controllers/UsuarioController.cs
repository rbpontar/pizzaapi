using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaApi.Models;
using PizzaApi.Services;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController
    {
        private UsuarioService usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Usuario usuario)
        {
            usuario = await usuarioService.Gravar(usuario);
            return new JsonResult(usuario);
        }


        [HttpGet("{IdUsuario}")]
        public async Task<IActionResult> BuscarPorId(int IdUsuario)
        {
            //Endereco endereco = new Endereco
            //{
            //    Logradouro = "Rua da Lapa ",
            //    Complemento = "apto 109",
            //    Numero = "300"
            //};


            //await usuarioService.Gravar(new Usuario
            //{
            //    Nome = "Rodolfo",
            //    Endereco = endereco,
            //    Login = "rodolfo",
            //    Senha = "1234"
            //});

            var usuarios = await usuarioService.Listar();

            return new JsonResult(usuarios);
        }

    }
}
