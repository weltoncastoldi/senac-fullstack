using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class Tag : Entidade
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Cor { get; private set; }

        public virtual Usuario Usuario { get; private set; }

        //Construtor
        public Tag(Guid usuarioId, string nome, string cor)
        {
            ValidarEntidadeTag(cor);

            UsuarioId = usuarioId;
            Nome = nome.ToLower();
            Cor = cor;
        }

        private void ValidarEntidadeTag(string cor)
        {
            if (string.IsNullOrEmpty(cor))
            {
                return; //retorna caso a cor seja nula ou vazia
            }

            //#FF02AB
            var corRegex = new Regex(@"^#?([0-9a-fA-F]{3}){1,2}$");

            if (!corRegex.IsMatch(cor))
            {
                throw new Exception("A cor deve estar no formato hexadecimal");
            }
        }
    }
}