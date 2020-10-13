using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaApi.Models;
using PizzaApi.Repositories;

namespace PizzaApi.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepo repository;

        public UsuarioService(ApiContext contexto)
        {
            repository = new UsuarioRepo(contexto);
        }

        public UsuarioService()
        {
        }

        public async Task<List<Usuario>> Listar()
        {
            try
            {
                return await repository.Listar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Usuario> GetById(int Id)
        {
            try
            {
                var usuario = await repository.BuscarPorId(Id);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado.");

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Usuario> Gravar(Usuario usuario)
        {
            try
            {

                return await repository.Gravar(usuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
