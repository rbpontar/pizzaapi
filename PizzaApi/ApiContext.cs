using System;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;

namespace PizzaApi
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ItemPedido> Items { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }


    }
}
