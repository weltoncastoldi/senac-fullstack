using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Commands
{
    public class InativarSubcategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o ID da subcategoria")]
        public required Guid SubcategoriaId { get; set; }
    }

    internal class InativarSubcategoriaCommandHandler : IRequestHandler<InativarSubcategoriaCommand, (string, bool)>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;

        public InativarSubcategoriaCommandHandler(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }
        
        public async Task<(string, bool)> Handle(InativarSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            var subcategoria =
                await _subcategoriaRepository.ObterPorIdAsync(request.SubcategoriaId);

            if (subcategoria == null)
            {
                return ("Subcategoria não encontrada", false);
            }

            subcategoria.Inativar();

            await _subcategoriaRepository.AtualizarAsync(subcategoria);
            return ("Subcategoria desativada com sucesso", true);
        }
    }

}
