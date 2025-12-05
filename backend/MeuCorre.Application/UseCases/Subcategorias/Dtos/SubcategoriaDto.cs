using MeuCorre.Domain.Enums;

namespace MeuCorre.Application.UseCases.Subcategorias.Dtos
{
    public record SubcategoriaDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public bool Ativo { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime? UltimaAlteracao { get; set; }
    }
}
