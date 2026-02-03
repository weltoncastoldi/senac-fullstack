using System.Text.RegularExpressions;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Entities
{
    public class Subcategoria : Entidade
    {
        public Guid UsuarioId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao  { get; private set; }
        public string? Cor { get; private set; }
        public string? Icone { get; private set; }
        public TipoTransacao TipoDaTransacao { get; private set; }
        public bool Ativo { get; private set; }

        // Propriedade de navegação para a entidade Usuario
        public virtual Usuario Usuario { get; private set; }

        // Propriedade de navegação para a entidade Categoria
        public virtual Categoria Categoria { get; private set; }

        public Subcategoria(Guid usuarioId, Guid categoriaId, string nome, TipoTransacao tipoDaTransacao, string? descricao, string? cor, string? icone)
        {
            ValidarEntidadeSubcategoria(cor);

            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipoDaTransacao;
            Ativo = true;
        }

        public void AtualizarInformacoes(string nome, TipoTransacao tipoDaTransacao,
                                         string descricao, string cor, string icone)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipoDaTransacao;
            AtualizarDataMoficacao();
        }

        public void Ativar()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }
        public void Inativar()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }

        private void ValidarEntidadeSubcategoria(string cor)
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
