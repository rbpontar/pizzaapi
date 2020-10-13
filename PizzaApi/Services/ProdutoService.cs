using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaApi.Models;
using PizzaApi.Repositories;

namespace PizzaApi.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepo repository;

        public ProdutoService(ApiContext contexto)
        {
            repository = new ProdutoRepo(contexto);
        }

        public ProdutoService()
        {
        }

        public async Task<List<Produto>> Listar()
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

        public async Task<Produto> GetById(int Id)
        {
            try
            {
                var produto = await repository.BuscarPorId(Id);

                if (produto == null)
                    throw new Exception("Produto não encontrado.");

                return produto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Produto> Gravar(Produto produto)
        {
            try
            {

                return await repository.Gravar(produto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
