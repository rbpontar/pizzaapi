﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PizzaApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }

        [MaxLength(10, ErrorMessage = "Tamanho nmáximo 10 caracteres")]
        public String Login { get; set; }

        [MaxLength(10, ErrorMessage = "Tamanho nmáximo 10 caracteres")]
        public String Senha { get; set; }

        public int IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]

        public virtual Endereco Endereco { get; set; }

        public Usuario()
        {
        }
    }
}