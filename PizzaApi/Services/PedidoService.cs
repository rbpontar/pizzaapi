using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaApi.Dtos;
using PizzaApi.Models;
using PizzaApi.Repositories;

namespace PizzaApi.Services
{
    public class PedidoService
    {
        private readonly PedidoRepo repository;
        private readonly UsuarioRepo usuarioRepo;
        private readonly ProdutoRepo produtoRepo;


        public PedidoService(ApiContext contexto)
        {
            this.repository = new PedidoRepo(contexto);
            this.usuarioRepo = new UsuarioRepo(contexto);
            this.produtoRepo = new ProdutoRepo(contexto);
        }

        public PedidoService()
        {
        }

        public async Task<List<Pedido>> ListarPedidosUsuario(int IdUsuario)
        {
            try
            {
                return await repository.ListarPedidosUsuario(IdUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Pedido> GetById(int Id)
        {
            try
            {
                var pedido = await repository.GetById(Id);

                if (pedido == null)
                    throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public async Task<Pedido> Gravar(PedidoDto pedidoDto)
        {
            try
            {
                var pedido = new Pedido();

                var itens = new List<ItemPedido>();

                foreach (ItemPedidoDto item in pedidoDto.Itens)
                {
                    var itemPedido = new ItemPedido
                    {
                        IdProduto1 = item.IdProduto1,
                        IdProduto2 = item.IdProduto2,
                    };

                    itens.Add(itemPedido);
                }

                if (pedidoDto.IdUsuario.HasValue)
                    pedido.Usuario = await usuarioRepo.BuscarPorId(pedidoDto.IdUsuario.Value);

                if (pedido.Usuario != null)
                {
                    pedido.Endereco = new Endereco
                    {
                        Logradouro = pedido.Usuario.Endereco.Logradouro,
                        Complemento = pedido.Usuario.Endereco.Complemento,
                        Numero = pedido.Usuario.Endereco.Numero
                    };
                }
                else if (pedidoDto.enderecoDto != null)
                    pedido.Endereco = new Endereco
                    {
                        Logradouro = pedidoDto.enderecoDto.Logradouro,
                        Complemento = pedidoDto.enderecoDto.Complemento,
                        Numero = pedidoDto.enderecoDto.Numero
                    };

                pedido.Itens.AddRange(itens);

                pedido.IsValid();

                foreach (ItemPedido item in pedido.Itens)
                {
                    item.Produto1 = await produtoRepo.BuscarPorId(item.IdProduto1);
                    item.Produto2 = await produtoRepo.BuscarPorId(item.IdProduto2);
                    item.Nome = string.Format("{0}{1}", item.Produto1.Nome, item.Produto2 != null ? " X " + item.Produto2.Nome : "");

                    pedido.Valor += Math.Round(item.Total, 2);
                }

                pedido.Data = DateTime.Now;
                pedido.Valor = Math.Round(pedido.Valor, 2);
                return await repository.Gravar(pedido);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
