﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaApi.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        public double Valor { get; set; }

        public Produto()
        {


        }

        public Produto(string nome, double valor)
        {
            Nome = nome;
            Valor = valor;
        }
    }
}