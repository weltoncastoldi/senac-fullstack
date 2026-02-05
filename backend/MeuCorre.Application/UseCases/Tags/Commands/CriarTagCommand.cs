using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Tags.Commands
{
    public class CriarTagCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id do usuário é obrigatório")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage ="Nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage ="Cor é obrigatório")]
        public required string Cor { get; set; }
    }

    internal class CriarTagCommandHandler : IRequestHandler<CriarTagCommand, (string, bool)>
    {
        public Task<(string, bool)> Handle(CriarTagCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}