using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Tags.Commands
{
    public class AtivarTagCommand : IRequest<(string, bool)>
    {

        [Required(ErrorMessage = "Id é obrigatório")]
        public required Guid Id { get; set; }

    }

    internal class AtivarTagCommandHandler : IRequestHandler<AtivarTagCommand, (string, bool)>
    {
        public Task<(string, bool)> Handle(AtivarTagCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
