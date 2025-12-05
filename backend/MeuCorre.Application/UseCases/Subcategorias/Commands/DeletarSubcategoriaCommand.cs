using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Commands
{
    public class DeletarSubcategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id do usuário")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da subcategoria")]
        public required Guid SubcategoriaId { get; set; }
    }
    internal class DeletarSubcategoriaCommandHandler : IRequestHandler<DeletarSubcategoriaCommand, (string, bool)>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        public DeletarSubcategoriaCommandHandler(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<(string, bool)> Handle(DeletarSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            var subcategoria = await _subcategoriaRepository.ObterPorIdAsync(request.SubcategoriaId);

            if (subcategoria == null)
                return ("Subcategoria não encontrada", false);

            if (subcategoria.UsuarioId != request.UsuarioId)
                return ("Subcategoria não pertence ao usuário", false);

            await _subcategoriaRepository.RemoverAsync(subcategoria);

            return ("Subcategoria removida com sucesso", true);
        }
    }


}
