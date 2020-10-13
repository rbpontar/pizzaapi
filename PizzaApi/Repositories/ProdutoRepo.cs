

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;

namespace PizzaApi.Repositories
{
    public class ProdutoRepo : GenericRepo<Produto>
    {
        public ProdutoRepo(ApiContext contexto) : base(contexto)
        {


        }

        public async Task<List<Produto>> Listar()
        {
            var produtos = await _contexto.Produtos             
                .ToListAsync();

            return produtos;
        }

        public async Task<Produto> BuscarPorId(int id)
        {
            var produto = await _contexto.Produtos
               .Where(p => p.Id == id)
               .SingleOrDefaultAsync();

            return produto;
        }


        public async Task<Produto> Gravar(Produto produto)
        {
            try
            {
                _contexto.Add(produto);

                await _contexto.SaveChangesAsync();

                return produto;

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
