using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Application.UseCases.Subcategorias.Dtos;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Queries
{
    public class ListarTodasSubcategoriasQuery : IRequest<IList<SubcategoriaDto>>
    {
        [Required(ErrorMessage = "Informe o Id do usuário")]
        public required Guid UsuarioId { get; set; }

        // Opcional: filtrar por categoria específica
        public Guid? CategoriaId { get; set; }
    }

    internal class ListarTodasSubcategoriasQueryHandler : IRequestHandler<ListarTodasSubcategoriasQuery, IList<SubcategoriaDto>>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        public ListarTodasSubcategoriasQueryHandler(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<IList<SubcategoriaDto>> Handle(ListarTodasSubcategoriasQuery request, CancellationToken cancellationToken)
        {
            IList<Domain.Entities.Subcategoria> listaSubcategorias;

            // Se foi informado o CategoriaId, filtra por categoria
            if (request.CategoriaId.HasValue)
            {
                listaSubcategorias = await _subcategoriaRepository.ListarPorCategoriaAsync(request.CategoriaId.Value);
            }
            else
            {
                listaSubcategorias = await _subcategoriaRepository.ListarTodasPorUsuarioAsync(request.UsuarioId);
            }

            if (listaSubcategorias == null)
                return null;

            var subcategorias = new List<SubcategoriaDto>();

            foreach (var sub in listaSubcategorias)
            {
                subcategorias.Add(new SubcategoriaDto
                {
                    Id = sub.Id,
                    CategoriaId = sub.CategoriaId,
                    Nome = sub.Nome,
                    Ativo = sub.Ativo,
                    Tipo = sub.TipoDaTransacao,
                    Cor = sub.Cor,
                    Descricao = sub.Descricao,
                    Icone = sub.Icone,
                    UltimaAlteracao = sub.DataAtualizacao
                });
            }

            return subcategorias;
        }
    }

}
