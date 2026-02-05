using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Tags.Commands
{
    public class DeletarTagCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id do usuário é obrigatório")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Id é obrigatório")]
        public required Guid Id { get; set; }

    }

    internal class DeletarTagCommandHandler : IRequestHandler<DeletarTagCommand, (string, bool)>
    {
        public Task<(string, bool)> Handle(DeletarTagCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
