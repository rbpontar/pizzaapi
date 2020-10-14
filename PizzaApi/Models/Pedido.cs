using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApi.Models
{
    public class Pedido
    {

        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public virtual List<ItemPedido> Itens { get; set; }

        //[Column("idusuariosolicitante"), Required]
        public int? IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }

        public int? IdEndereco { get; set; }
        [ForeignKey(nameof(IdEndereco))]
        public virtual Endereco Endereco { get; set; }

        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }

        public void IsValid()
        {
            if (Itens.Count > 10 || Itens.Count == 0)
                throw new Exception("Pedido deve ter no mínimo 1 e no máximo 10 pizzas.");

            Endereco endereco = null;

            if (Usuario != null)
                endereco = Usuario.Endereco;
            else if (Endereco != null)
                endereco = Endereco;

            if (endereco == null)
                throw new Exception("Informar os dados da entrega.");
        }
    }
}
