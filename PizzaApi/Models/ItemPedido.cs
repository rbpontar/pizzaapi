
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApi.Models
{
    public class ItemPedido
    {

        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        public double Valor { get; set; }
        //public double Quantidade { get; set; }

        public int IdProduto1 { get; set; }

        [ForeignKey(nameof(IdProduto1))]

        public virtual Produto Produto1 { get; set; }

        public int IdProduto2 { get; set; }

        [ForeignKey(nameof(IdProduto2))]

        public virtual Produto Produto2 { get; set; }

        //[NotMapped]
        public double Total
        {
            get
            {
                var fator = Produto1 != null && Produto2 != null ? 0.5 : 1;

                double v1 = Produto1 != null ? Produto1.Valor * fator : 0;
                double v2 = Produto2 != null ? Produto2.Valor * fator : 0;

                return Math.Round(v1 + v2, 2);
            }
        }

        public ItemPedido()
        {
        }
    }
}
