

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;

namespace PizzaApi.Repositories
{
    public class UsuarioRepo : GenericRepo<Usuario>
    {
        public UsuarioRepo(ApiContext contexto) : base(contexto)
        {


        }

        public async Task<List<Usuario>> Listar()
        {
            var usuarios = await _contexto.Usuarios             
                .ToListAsync();

            return usuarios;
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            var usuario = await _contexto.Usuarios
               .Where(p => p.Id == id).Include(e => e.Endereco)
               .SingleOrDefaultAsync();

            return usuario;
        }


        public async Task<Usuario> Gravar(Usuario usuario)
        {
            try
            {
                _contexto.Add(usuario);

                await _contexto.SaveChangesAsync();

                return usuario;

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
