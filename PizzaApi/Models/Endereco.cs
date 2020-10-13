using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaApi.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public String Logradouro { get; set; }
        public String Numero { get; set; }
        public String Complemento { get; set; }


        public Endereco()
        {
        }
    }
}
