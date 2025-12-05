using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Application.UseCases.Subcategorias.Dtos;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Queries
{
    public class ObterSubcategoriaQuery : IRequest<SubcategoriaDto>
    {
        [Required(ErrorMessage = "Informe o Id da subcategoria")]
        public required Guid SubcategoriaId { get; set; }
    }

    internal class ObterSubcategoriaQueryHandler : IRequestHandler<ObterSubcategoriaQuery, SubcategoriaDto>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        public ObterSubcategoriaQueryHandler(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<SubcategoriaDto> Handle(ObterSubcategoriaQuery request, CancellationToken cancellationToken)
        {
            var subcategoria = await _subcategoriaRepository.ObterPorIdAsync(request.SubcategoriaId);

            if (subcategoria == null)
                return null;

            var subcategoriaDto = new SubcategoriaDto
            {
                Id = subcategoria.Id,
                CategoriaId = subcategoria.CategoriaId,
                Nome = subcategoria.Nome,
                Ativo = subcategoria.Ativo,
                Tipo = subcategoria.TipoDaTransacao,
                Cor = subcategoria.Cor,
                Descricao = subcategoria.Descricao,
                Icone = subcategoria.Icone,
            };

            return subcategoriaDto;
        }
    }
}
