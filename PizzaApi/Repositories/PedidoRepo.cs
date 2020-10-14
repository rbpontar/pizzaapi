using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;

namespace PizzaApi.Repositories
{
    public class PedidoRepo : GenericRepo<Pedido>
    {
        public PedidoRepo(ApiContext contexto) : base(contexto)
        {


        }

        public async Task<List<Pedido>> ListarPedidosUsuario(int idUsuario)
        {
            var pedidos = await _contexto.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.IdUsuario == idUsuario)
                .ToListAsync();

            foreach (Pedido pedido in pedidos)
            {
                foreach (ItemPedido item in pedido.Itens)
                {
                    item.Produto1 = await _contexto.Produtos.Where(p => p.Id == item.IdProduto1).SingleOrDefaultAsync();
                    item.Produto2 = await _contexto.Produtos.Where(p => p.Id == item.IdProduto2).SingleOrDefaultAsync();
                }
            }

            return pedidos;
        }

        public async Task<Pedido> Gravar(Pedido pedido)
        {
            try
            {
                _contexto.Add(pedido);

                await _contexto.SaveChangesAsync();

                return pedido;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<Pedido> GetById(int id)
        {
            var pedido = await _contexto.Pedidos
               .Include(p => p.Itens)
               .Where(p => p.Id == id)
               .SingleOrDefaultAsync();

            if (pedido != null)
            {
                foreach (ItemPedido item in pedido.Itens)
                {
                    item.Produto1 = await _contexto.Produtos.Where(p => p.Id == item.IdProduto1).SingleOrDefaultAsync();
                    item.Produto2 = await _contexto.Produtos.Where(p => p.Id == item.IdProduto2).SingleOrDefaultAsync();
                }
            }

            return pedido;
        }
    }
}
