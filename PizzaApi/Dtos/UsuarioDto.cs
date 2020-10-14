using System;
namespace PizzaApi.Dtos
{
    public class UsuarioDto
    {
        public String Nome { get; set; }

        public int IdEndereco { get; set; }

        public virtual EnderecoDto Endereco { get; set; }

        public UsuarioDto()
        {
        }
    }
}
