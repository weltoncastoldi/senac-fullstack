using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Commands
{
    public class AtivarSubcategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o ID da subcategoria")]
        public required Guid SubcategoriaId { get; set; }
    }
    internal class AtivarSubcategoriaCommandHandler : IRequestHandler<AtivarSubcategoriaCommand, (string, bool)>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;

        public AtivarSubcategoriaCommandHandler(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }


        public async Task<(string, bool)> Handle(AtivarSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            var subcategoria = 
                await _subcategoriaRepository.ObterPorIdAsync(request.SubcategoriaId);

            if (subcategoria == null)
            {
                return ("Subcategoria não encontrada", false);
            }

            subcategoria.Ativar();

            await _subcategoriaRepository.AtualizarAsync(subcategoria);
            return ("Subcategoria ativada com sucesso", true);
        }
    }
}
