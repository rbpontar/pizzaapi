using System.Collections.Generic;
using PizzaApi.Models;

namespace PizzaApi.Dtos
{
    public class PedidoDto
    {
        public int? IdUsuario { get; set; }
        public EnderecoDto enderecoDto { get; set; }
        public List<ItemPedidoDto> Itens { get; set; }
    }
}
